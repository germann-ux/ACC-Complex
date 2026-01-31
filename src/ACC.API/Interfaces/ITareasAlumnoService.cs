using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces;

public interface ITareasAlumnoService
{
    Task<ServiceResult<TareasPendientesResumenDto>> GetResumenPendientesAsync(
        string userId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<List<TareaAlumnoListadoDto>>> GetListadoAsync(
        string userId,
        CancellationToken cancellationToken = default);
}
