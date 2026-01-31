using ACC.API.Interfaces;
using ACC.Data;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services;

public class TareasAlumnoService : ITareasAlumnoService
{
    private readonly ACCDbContext _dbContext;

    public TareasAlumnoService(ACCDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResult<TareasPendientesResumenDto>> GetResumenPendientesAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return ServiceResult<TareasPendientesResumenDto>.Fail("El id del usuario no puede estar vacío.");
        }

        var hoy = DateTime.UtcNow.Date;

        var asignaciones = await _dbContext.TareasAsignaciones
            .AsNoTracking()
            .Include(a => a.Tarea)
            .Where(a => a.UsuarioId == userId)
            .ToListAsync(cancellationToken);

        var pendientes = asignaciones
            .Where(a => a.Tarea != null)
            .Where(a => a.Estado != TareaEstado.Completada)
            .ToList();

        var resumen = new TareasPendientesResumenDto
        {
            Vencidas = pendientes.Count(a => a.Tarea!.FechaLimite.Date < hoy),
            ParaHoy = pendientes.Count(a => a.Tarea!.FechaLimite.Date == hoy),
            Proximas = pendientes.Count(a => a.Tarea!.FechaLimite.Date > hoy)
        };

        return ServiceResult<TareasPendientesResumenDto>.Ok(resumen);
    }

    public async Task<ServiceResult<List<TareaAlumnoListadoDto>>> GetListadoAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return ServiceResult<List<TareaAlumnoListadoDto>>.Fail("El id del usuario no puede estar vacío.");
        }

        var asignaciones = await _dbContext.TareasAsignaciones
            .AsNoTracking()
            .Include(a => a.Tarea)
            .Where(a => a.UsuarioId == userId)
            .OrderBy(a => a.Tarea.FechaLimite)
            .ToListAsync(cancellationToken);

        if (asignaciones.Count == 0)
        {
            return ServiceResult<List<TareaAlumnoListadoDto>>.Ok([]);
        }

        var listado = asignaciones
            .Where(a => a.Tarea != null)
            .Select(a => new TareaAlumnoListadoDto
            {
                TareaId = a.TareaId,
                TareaAsignacionId = a.Id,
                Titulo = a.Tarea.Titulo,
                EnunciadoCorto = ObtenerEnunciadoCorto(a.Tarea.Enunciado, 140),
                FechaLimite = a.Tarea.FechaLimite,
                Estado = a.Estado,
                EstadoEntrega = a.EstadoEntrega,
                Calificacion = a.Calificacion
            })
            .ToList();

        return ServiceResult<List<TareaAlumnoListadoDto>>.Ok(listado);
    }

    private static string? ObtenerEnunciadoCorto(string? enunciado, int max)
    {
        if (string.IsNullOrWhiteSpace(enunciado))
        {
            return null;
        }

        var limpio = enunciado.Trim();
        if (limpio.Length <= max)
        {
            return limpio;
        }

        return $"{limpio[..max]}…";
    }
}
