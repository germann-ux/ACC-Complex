using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly ACCDbContext _context;

        public NotificacionService(ACCDbContext context)
        {
            _context = context;
        }

        public Task<NotificacionDto> CreateNotificacionAsync(NotificacionDto notificacion)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteNotificacionAsync(int notificacionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificacionDto>> GetAllNotificacionesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<NotificacionDto> GetNotificacionByIdAsync(int notificacionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NotificacionDto>> GetNotificacionesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<NotificacionDto> UpdateNotificacionAsync(NotificacionDto notificacion)
        {
            throw new NotImplementedException();
        }
    }
}





