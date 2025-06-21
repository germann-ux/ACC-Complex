using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces
{
    public interface IBibliotecaService
    {
        Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerContenidosAsync();
        Task<ServiceResult<ContenidoCapituloDto>> ObtenerCapituloAsync(int Id); 
    }
}
