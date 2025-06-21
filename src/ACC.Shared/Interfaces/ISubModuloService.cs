using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface ISubModuloService
    {
        // Crear un nuevo submódulo
        Task<SubModuloDto> CreateSubModuloAsync(SubModuloDto subModulo);

        // Obtener un submódulo por su ID
        Task<SubModuloDto> GetSubModuloByIdAsync(int subModuloId);

        // Obtener todos los submódulos
        Task<IEnumerable<SubModuloDto>> GetAllSubModulosAsync();

        // Actualizar un submódulo existente
        Task<SubModuloDto> UpdateSubModuloAsync(SubModuloDto subModulo);

        // Eliminar un submódulo por su ID
        Task<bool> DeleteSubModuloAsync(int subModuloId);

        // Buscar submódulos por nombre
        Task<IEnumerable<SubModuloDto>> SearchSubModulosByNameAsync(string nombreSubModulo);

        // Obtener submódulos por módulo
        Task<List<SubModuloDto>> GetSubModulosPorModuloAsync(int idModulo);
    }
}




