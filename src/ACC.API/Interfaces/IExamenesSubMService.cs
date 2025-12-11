using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces; 

public interface IExamenesSubMService
{
    Task<ServiceResult<List<ExamenSubModuloDto>>> ObtenerExamenesSubMAsync();
    Task<ServiceResult<ExamenSubModuloDto?>> ObtenerExamenSubMAsync(int id);
}
