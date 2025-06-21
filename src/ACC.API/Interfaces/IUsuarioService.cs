using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.API.Interfaces
{
    public interface IUsuarioService
    {
        // sirve para mantener el usuario sincronizado con la base de datos de identity en WebApp
        Task<ServiceResult<ApplicationUserDto>> SincUserAsync(ApplicationUserDto dto);

        // navegacion de usuarios
        Task<ServiceResult<ApplicationUserDto>> GetUserByIdAsync(string userId);
        
        // navegacion de usuarios
        Task<ServiceResult<ApplicationUserDto>> GetUserByNameAsync(string userName);
        
        // obtener el progreso del usuario
        Task<ServiceResult<double>> GetProgresoUserByIdAsync(string idUsuario);
    }
}
