using System.Collections.Generic;
using System.Threading.Tasks;
using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.Shared.Interfaces
{
    public interface IModuloService
    {
        // Crear un nuevo módulo
        Task<ModuloDto> CreateModuloAsync(ModuloDto modulo);

        // Obtener un módulo por su ID
        Task<ModuloDto> GetModuloByIdAsync(int moduloId);

        // Obtener todos los módulos
        Task<ServiceResult<IEnumerable<ModuloDto>>> GetAllModulosAsync();

        // Actualizar un módulo existente
        Task<ModuloDto> UpdateModuloAsync(ModuloDto modulo);

        // Eliminar un módulo por su ID
        Task<bool> DeleteModuloAsync(int moduloId);

        // Buscar módulos por nombre
        Task<IEnumerable<ModuloDto>> SearchModulosByNameAsync(string nombreModulo);

        // localizar el progreso de un usuario en el modulo, por medio de el id de usuario
        Task<UsuarioModulosDto> GetProgresoUsuarioModulos(string IdUsuario, int IdModulo); // todavia no funciona, el problema surge en la autorizacion

        Task<ModuloDto?> GetModuloSeleccionadoAsync();
    }
}