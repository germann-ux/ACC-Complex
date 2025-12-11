using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces;
/// <summary>
/// Interfaz de servicio para la gestión de evaluaciones.
/// </summary>
public interface IEvaluacionesService
{
    Task<ServiceResult<IReadOnlyList<EvaluacionListadoDto>>> GetByAulaAsync(
        int aulaId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<EvaluacionListadoDto>> CrearAsync(
        int aulaId,
        EvaluacionCreateDto createDto,
        string docenteId,
        CancellationToken cancellationToken = default);
}
