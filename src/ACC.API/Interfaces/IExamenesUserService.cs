using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;

namespace ACC.API.Interfaces;

/// <summary>
/// Contrato para el servicio que maneja los intentos de examenes por usuario
/// </summary>
public interface IExamenesUserService
{
    Task<ServiceResult<List<ExamenIntentoDto>>> ObtenerIntentosPorUsuarioAsync(string userId);
    Task<ServiceResult<ExamenEstadoDto?>> ObtenerEstadoExamenAsync(string userId, ExamenTipo tipo, int examenId);
    Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoPorUsuarioYExamenAsync(string userId, ExamenTipo tipo, int examenId);
    Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoPorUsuarioYExamenAsync(string userId, int examenSubModuloId);
    Task<ServiceResult<ExamenIntentoDto>> RegistrarIntentoAsync(ExamenIntentoDto intentoDto);
    Task<ServiceResult<int?>> ObtenerUmbralAprobacionAsync(ExamenIntento examen);
}
