using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface INotificacionService
    {
        // Crear una nueva notificación
        Task<NotificacionDto> CreateNotificacionAsync(NotificacionDto notificacion);

        // Obtener una notificación por su ID
        Task<NotificacionDto> GetNotificacionByIdAsync(int notificacionId);

        // Obtener todas las notificaciones
        Task<IEnumerable<NotificacionDto>> GetAllNotificacionesAsync();

        // Actualizar una notificación existente
        Task<NotificacionDto> UpdateNotificacionAsync(NotificacionDto notificacion);

        // Eliminar una notificación por su ID
        Task<bool> DeleteNotificacionAsync(int notificacionId);

        // Buscar notificaciones por usuario
        Task<IEnumerable<NotificacionDto>> GetNotificacionesByUserIdAsync(string userId);
    }
}




