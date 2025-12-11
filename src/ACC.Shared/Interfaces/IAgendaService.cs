using ACC.Shared.DTOs;
using System.Diagnostics;
namespace ACC.Shared.Interfaces
{
    public interface IAgendaService
    {
        // Obtener la agenda de un usuario
        Task<AgendaDto> GetAgendaByUserAsync(string userId);

        // Crear una nueva agenda
        Task<AgendaDto> CreateAgendaAsync(AgendaDto agenda, string IdUsuario);

        // Eliminar una agenda por ID
        Task<bool> DeleteAgendaAsync(int agendaId);
    }
}
