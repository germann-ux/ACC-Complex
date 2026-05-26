using ACC.API.Interfaces;
using ACC.Data;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services;

public class AulasEstudiantesService : IAulasEstudiantesService
{
    private readonly ACCDbContext _context;

    public AulasEstudiantesService(ACCDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<IReadOnlyList<EstudianteListadoDto>>> GetEstudiantesAsync(
        int aulaId,
        string currentUserId,
        bool esAdministrador = false,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Fail("El ID del aula es inválido.");
            }

            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Unauthorized("Usuario no autenticado.");
            }

            var aula = await _context.Aulas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.NotFound("El aula no existe.");
            }

            if (!esAdministrador && !string.Equals(aula.DocenteId, currentUserId, StringComparison.Ordinal))
            {
                return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Forbidden("No tienes permisos para consultar los estudiantes de esta aula.");
            }

            var estudiantes = await _context.AulaEstudiantes
                .AsNoTracking()
                .Where(ae => ae.AulaId == aulaId)
                .Join(
                    _context.Usuarios.AsNoTracking(),
                    ae => ae.UsuarioId,
                    u => u.Id,
                    (ae, u) => new
                    {
                        u.Id,
                        Nombre = u.Nombre ?? string.Empty,
                        Correo = u.Email ?? string.Empty
                    })
                .OrderBy(x => x.Nombre)
                .ToListAsync(cancellationToken);

            if (estudiantes.Count == 0)
            {
                return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Ok(Array.Empty<EstudianteListadoDto>(), "No hay estudiantes inscritos en esta aula.");
            }

            var resultado = new List<EstudianteListadoDto>(estudiantes.Count);

            if (aula.SubModuloId.HasValue)
            {
                var subModuloId = aula.SubModuloId.Value;
                var totalSubTemas = await _context.SubTemas
                    .AsNoTracking()
                    .CountAsync(st => st.Tema != null && st.Tema.Id_SubModulo == subModuloId, cancellationToken);

                var usuarioIds = estudiantes.Select(x => x.Id).ToList();
                var progresos = await _context.ProgresoUsuarios
                    .AsNoTracking()
                    .Include(p => p.SubTema)
                        .ThenInclude(st => st!.Tema)
                    .Where(p => usuarioIds.Contains(p.UsuarioId)
                                && p.SubTema != null
                                && p.SubTema.Tema != null
                                && p.SubTema.Tema.Id_SubModulo == subModuloId)
                    .OrderByDescending(p => p.Fecha)
                    .ToListAsync(cancellationToken);

                foreach (var estudiante in estudiantes)
                {
                    var progresosDelEstudiante = progresos.Where(p => p.UsuarioId == estudiante.Id).ToList();
                    var completados = progresosDelEstudiante.Count(p => p.Completado);
                    var porcentaje = totalSubTemas > 0
                        ? (int)Math.Round((double)completados * 100 / totalSubTemas, MidpointRounding.AwayFromZero)
                        : 0;

                    resultado.Add(new EstudianteListadoDto
                    {
                        Nombre = estudiante.Nombre,
                        Correo = estudiante.Correo,
                        Progreso = Math.Clamp(porcentaje, 0, 100),
                        UltimaLeccion = progresosDelEstudiante.FirstOrDefault()?.SubTema?.NombreSubTema ?? string.Empty
                    });
                }
            }
            else
            {
                foreach (var estudiante in estudiantes)
                {
                    resultado.Add(new EstudianteListadoDto
                    {
                        Nombre = estudiante.Nombre,
                        Correo = estudiante.Correo,
                        Progreso = 0,
                        UltimaLeccion = string.Empty
                    });
                }
            }

            return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Ok(resultado, "Estudiantes del aula obtenidos correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<IReadOnlyList<EstudianteListadoDto>>.Error(ex);
        }
    }
}
