using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces
{
    public interface IBibliotecaService
    {
        Task<ServiceResult<List<CapituloDto>>> ObtenerCapitulosAsync();
        Task<ServiceResult<CapituloDto>> ObtenerCapituloPorIdAsync(int idCapitulo);
        Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerContenidosRecomendadosRandomAsync(int count, int? maxIdContenido);
    }
}
