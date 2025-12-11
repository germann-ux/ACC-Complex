using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces; 

public interface IExamenesModService
{
    Task<ServiceResult<List<ExamenModuloDto>>> ObtenerExamenesModAsync();
    Task<ServiceResult<ExamenModuloDto?>> ObtenerExamenModAsync(int id); 
}
