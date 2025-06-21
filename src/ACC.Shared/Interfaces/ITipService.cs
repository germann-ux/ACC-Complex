using System.Threading.Tasks;
using ACC.Shared.DTOs; 
namespace ACC.Shared.Interfaces
{
    public interface ITipService
    {
        Task<TipDto> ObtenerTipAleatorioAsync();
    }
}
