using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces;

public interface ILeccionesAdminService
{
    Task<ServiceResult<List<LeccionAdminDto>>> ListarAsync(int? subTemaId, CancellationToken cancellationToken = default);

    Task<ServiceResult<LeccionAdminDto>> ObtenerAsync(int idLeccion, CancellationToken cancellationToken = default);

    Task<ServiceResult<LeccionAdminDto>> CrearAsync(LeccionAdminDto dto, CancellationToken cancellationToken = default);

    Task<ServiceResult<LeccionAdminDto>> ActualizarAsync(int idLeccion, LeccionAdminDto dto, CancellationToken cancellationToken = default);

    Task<ServiceResult> PublicarAsync(int idLeccion, CancellationToken cancellationToken = default);
}
