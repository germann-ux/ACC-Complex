using ACC.API.Interfaces;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services;

public class TareasAulaService : ITareasService
{
    private readonly ACCDbContext _dbContext;
    private readonly IMapper _mapper;

    public TareasAulaService(ACCDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResult<IReadOnlyList<TareaListadoDto>>> GetByAulaAsync(
        int aulaId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<IReadOnlyList<TareaListadoDto>>.Fail("El id del aula no es válido.");
            }

            var tareas = await _dbContext.Tareas
                .AsNoTracking()
                .Where(t => t.AulaId == aulaId)
                .OrderBy(t => t.FechaLimite)
                .ToListAsync(cancellationToken);

            var tareasDto = _mapper.Map<List<TareaListadoDto>>(tareas);
            return ServiceResult<IReadOnlyList<TareaListadoDto>>.Ok(tareasDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<IReadOnlyList<TareaListadoDto>>.Error(ex);
        }
    }

    public async Task<ServiceResult<IReadOnlyList<TareaListadoDto>>> GetByAulaDocenteAsync(
        int aulaId,
        string docenteId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<IReadOnlyList<TareaListadoDto>>.Fail("El id del aula no es válido.");
            }

            if (string.IsNullOrWhiteSpace(docenteId))
            {
                return ServiceResult<IReadOnlyList<TareaListadoDto>>.Unauthorized("No se pudo identificar al docente autenticado.");
            }

            var aula = await _dbContext.Aulas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<IReadOnlyList<TareaListadoDto>>.NotFound("El aula no fue encontrada.");
            }

            if (!string.Equals(aula.DocenteId, docenteId, StringComparison.Ordinal))
            {
                return ServiceResult<IReadOnlyList<TareaListadoDto>>.Forbidden("No tienes permisos para consultar tareas de esta aula.");
            }

            return await GetByAulaAsync(aulaId, cancellationToken);
        }
        catch (Exception ex)
        {
            return ServiceResult<IReadOnlyList<TareaListadoDto>>.Error(ex);
        }
    }

    public async Task<ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>> GetAlumnosAsignablesAsync(
        int aulaId,
        string docenteId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.Fail("El id del aula no es válido.");
            }

            if (string.IsNullOrWhiteSpace(docenteId))
            {
                return ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.Unauthorized("No se pudo identificar al docente autenticado.");
            }

            var aula = await _dbContext.Aulas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.NotFound("El aula no fue encontrada.");
            }

            if (!string.Equals(aula.DocenteId, docenteId, StringComparison.Ordinal))
            {
                return ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.Forbidden("No tienes permisos para consultar estudiantes de esta aula.");
            }

            var estudiantes = await _dbContext.AulaEstudiantes
                .AsNoTracking()
                .Where(ae => ae.AulaId == aulaId)
                .Join(
                    _dbContext.Usuarios.AsNoTracking(),
                    ae => ae.UsuarioId,
                    u => u.Id,
                    (ae, u) => new TareaAlumnoAsignableDto
                    {
                        UsuarioId = u.Id,
                        Nombre = u.Nombre ?? string.Empty,
                        Correo = u.Email ?? string.Empty
                    })
                .OrderBy(x => x.Nombre)
                .ToListAsync(cancellationToken);

            return ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.Ok(estudiantes);
        }
        catch (Exception ex)
        {
            return ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>.Error(ex);
        }
    }

    public async Task<ServiceResult<TareaListadoDto>> CrearAsync(
        int aulaId,
        TareaCreateDto createDto,
        string docenteId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (aulaId <= 0)
            {
                return ServiceResult<TareaListadoDto>.Fail("El id del aula no es válido.");
            }

            if (string.IsNullOrWhiteSpace(docenteId))
            {
                return ServiceResult<TareaListadoDto>.Unauthorized("No se pudo identificar al docente autenticado.");
            }

            if (createDto == null || string.IsNullOrWhiteSpace(createDto.Titulo))
            {
                return ServiceResult<TareaListadoDto>.Fail("El título de la tarea no puede estar vacío.");
            }

            if (string.IsNullOrWhiteSpace(createDto.Enunciado))
            {
                return ServiceResult<TareaListadoDto>.Fail("El enunciado de la tarea no puede estar vacío.");
            }

            if (createDto.FechaLimite == default)
            {
                return ServiceResult<TareaListadoDto>.Fail("La fecha límite es requerida.");
            }

            var aula = await _dbContext.Aulas
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);

            if (aula == null)
            {
                return ServiceResult<TareaListadoDto>.NotFound("El aula no fue encontrada.");
            }

            if (!string.Equals(aula.DocenteId, docenteId, StringComparison.Ordinal))
            {
                return ServiceResult<TareaListadoDto>.Forbidden("No tienes permisos para crear tareas en esta aula.");
            }

            var usuariosAsignadosResult = await ResolveUsuariosAsignadosAsync(aulaId, createDto, cancellationToken);
            if (!usuariosAsignadosResult.Success)
            {
                return ServiceResult<TareaListadoDto>.Fail(usuariosAsignadosResult.Message ?? "No se pudieron resolver los alumnos asignados.");
            }

            var usuarioIds = usuariosAsignadosResult.Data ?? [];
            if (usuarioIds.Count == 0)
            {
                return ServiceResult<TareaListadoDto>.Fail("No hay alumnos para asignar en el aula.");
            }

            var tarea = _mapper.Map<Tarea>(createDto);
            tarea.AulaId = aulaId;
            tarea.CreatedAt = DateTime.UtcNow;

            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            _dbContext.Tareas.Add(tarea);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var asignaciones = usuarioIds.Select(usuarioId => new TareasAsignaciones
            {
                TareaId = tarea.TareaId,
                UsuarioId = usuarioId,
                Estado = TareaEstado.NoIniciada,
                EstadoEntrega = TareaEstadoEntrega.NoEntregada
            });

            await _dbContext.TareasAsignaciones.AddRangeAsync(asignaciones, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var tareaDto = _mapper.Map<TareaListadoDto>(tarea);
            return ServiceResult<TareaListadoDto>.Ok(tareaDto);
        }
        catch (Exception ex)
        {
            return ServiceResult<TareaListadoDto>.Error(ex);
        }
    }

    public async Task<ServiceResult<bool>> ActualizarAsignacionAsync(
        int tareaId,
        string usuarioId,
        TareaAsignacionUpdateDto updateDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (tareaId <= 0)
            {
                return ServiceResult<bool>.Fail("El id de la tarea no es válido.");
            }

            if (string.IsNullOrWhiteSpace(usuarioId))
            {
                return ServiceResult<bool>.Fail("El id del usuario no puede estar vacío.");
            }

            if (updateDto == null)
            {
                return ServiceResult<bool>.Fail("El cuerpo de actualización es requerido.");
            }

            var asignacion = await _dbContext.TareasAsignaciones
                .Include(a => a.Tarea)
                .FirstOrDefaultAsync(a => a.TareaId == tareaId && a.UsuarioId == usuarioId, cancellationToken);

            if (asignacion == null)
            {
                return ServiceResult<bool>.NotFound("La asignación de la tarea no fue encontrada.");
            }

            _mapper.Map(updateDto, asignacion);

            if (asignacion.Estado == TareaEstado.Completada)
            {
                asignacion.FechaEntrega ??= DateTime.UtcNow;
                var fechaLimite = asignacion.Tarea.FechaLimite;
                asignacion.EstadoEntrega = asignacion.FechaEntrega <= fechaLimite
                    ? TareaEstadoEntrega.Entregada
                    : TareaEstadoEntrega.EntregadaTarde;
            }
            else
            {
                asignacion.EstadoEntrega = TareaEstadoEntrega.NoEntregada;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<bool>.Ok(true);
        }
        catch (Exception ex)
        {
            return ServiceResult<bool>.Error(ex);
        }
    }

    private async Task<ServiceResult<List<string>>> ResolveUsuariosAsignadosAsync(
        int aulaId,
        TareaCreateDto createDto,
        CancellationToken cancellationToken)
    {
        if (createDto.Scope == TareaScope.AulaCompleta)
        {
            var idsAulaCompleta = await _dbContext.AulaEstudiantes
                .AsNoTracking()
                .Where(ae => ae.AulaId == aulaId)
                .Select(ae => ae.UsuarioId)
                .Distinct()
                .ToListAsync(cancellationToken);

            return ServiceResult<List<string>>.Ok(idsAulaCompleta);
        }

        var solicitados = (createDto.UsuarioIds ?? [])
            .Where(id => !string.IsNullOrWhiteSpace(id))
            .Select(id => id.Trim())
            .Distinct(StringComparer.Ordinal)
            .ToList();

        if (solicitados.Count == 0)
        {
            return ServiceResult<List<string>>.Fail("Debes especificar al menos un usuario cuando el scope es Subconjunto.");
        }

        var alumnosValidos = await _dbContext.AulaEstudiantes
            .AsNoTracking()
            .Where(ae => ae.AulaId == aulaId && solicitados.Contains(ae.UsuarioId))
            .Select(ae => ae.UsuarioId)
            .Distinct()
            .ToListAsync(cancellationToken);

        var faltantes = solicitados.Except(alumnosValidos, StringComparer.Ordinal).ToList();
        if (faltantes.Count > 0)
        {
            return ServiceResult<List<string>>.Fail("Uno o más usuarios no pertenecen al aula.");
        }

        return ServiceResult<List<string>>.Ok(alumnosValidos);
    }
}
