using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces;
/// <summary>
/// Interfaz de servicio para la gestión de estudiantes en aulas.
/// </summary>
public interface IAulasEstudiantesService
{
    Task<ServiceResult<IReadOnlyList<EstudianteListadoDto>>> GetEstudiantesAsync(
        int aulaId,
        CancellationToken cancellationToken = default);
}
