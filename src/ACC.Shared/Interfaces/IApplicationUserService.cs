using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface IApplicationUserService // logica de negocio, no academico
    {
        // registrar un nuevo usuario
        Task<ServiceResult<ServiceResult<ApplicationUserDto>>>RegistrarUsuarioAsync(ApplicationUserDto dto);

        // Obtener un usuario por su ID
        Task<ServiceResult<ServiceResult<ApplicationUserDto>>>GetUserByIdAsync(string userId);

        // Obtener un usuario por su nombre
        Task<ServiceResult<ApplicationUserDto>> GetUserByNameAsync(string userName);

        // Obtener la lista de usuarios
        Task<IEnumerable<ServiceResult<ApplicationUserDto>>> GetAllUsersAsync();

        // Obtener el ID de un usuario por medio de claims de identidad
        Task<string> GetUserIdByClaimsAsync(ClaimsPrincipal claims);
    }
}

