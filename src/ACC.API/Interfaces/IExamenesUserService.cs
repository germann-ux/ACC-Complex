using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces; 

/// <summary>
/// Contrato para el servicio que maneja los intentos de examenes por usuario
/// </summary>
public interface IExamenesUserService
{
    Task<ServiceResult<List<ExamenIntentoDto>>> ObtenerIntentosPorUsuarioAsync(string userId);
    Task<ServiceResult<ExamenIntentoDto?>> ObtenerUltimoIntentoPorUsuarioYExamenAsync(string userId, int examenSubModuloId);
    Task<ServiceResult<ExamenIntentoDto>> RegistrarIntentoAsync(ExamenIntentoDto intentoDto);
    Task<ServiceResult<int?>> ObtenerUmbralAprobacionAsync(ExamenIntento examen); 
}
