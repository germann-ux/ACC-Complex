using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces;
/// <summary>
/// Interfaz de servicio para la gestión de anuncios.
/// </summary>
public interface IAnunciosService
{
    Task<ServiceResult<IReadOnlyList<AnuncioDto>>> GetByAulaAsync(
        int aulaId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<AnuncioDto>> CrearAsync(
        int aulaId,
        AnuncioCreateDto createDto,
        string docenteId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<AnuncioDto?>> ActualizarAsync(
        int aulaId,
        int anuncioId,
        AnuncioUpdateDto updateDto,
        string docenteId,
        CancellationToken cancellationToken = default);

    Task<ServiceResult<bool>> EliminarAsync(
        int aulaId,
        int anuncioId,
        string docenteId,
        CancellationToken cancellationToken = default);
}
