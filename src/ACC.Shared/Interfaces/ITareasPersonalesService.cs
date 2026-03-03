using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.Shared.Interfaces;

public interface ITareasPersonalesService
{
    Task<ServiceResult<TareaPersonalDto>> CreateTareaPersonalAsync(TareaPersonalDto tareaPersonal, string idUsuario);
    Task<ServiceResult<bool>> DeleteTareaPersonalAsync(int tareaPersonalId, string userId);
    Task<ServiceResult<TareaPersonalDto>> GetTareaPersonalByUserAsync(int idTareaPersonal, string userId);
    Task<ServiceResult<TareaPersonalDto>> UpdateTareaPersonalAsync(TareaPersonalDto tareaPersonal, string userId);
    Task<ServiceResult<List<TareaPersonalDto>>> GetTareasPersonalesByUserAsync(string userId);
}
