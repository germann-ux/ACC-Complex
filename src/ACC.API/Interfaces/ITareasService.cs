using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces;
/// <summary>
/// Interfaz de servicio para la gestión de tareas.
/// </summary>
public interface ITareasService
{
    Task<ServiceResult<IReadOnlyList<TareaListadoDto>>> GetByAulaAsync(
        int aulaId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<TareaListadoDto>> CrearAsync(
        int aulaId,
        TareaCreateDto createDto,
        string docenteId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<bool>> ActualizarAsignacionAsync(
        int tareaId,
        string usuarioId,
        TareaAsignacionUpdateDto updateDto,
        CancellationToken cancellationToken = default);
}
