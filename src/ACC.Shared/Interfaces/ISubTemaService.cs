using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface ISubTemaService // interface de los subtemas
    {
        // Crear un nuevo subtema
        Task<SubTemaDto> CreateSubTemaAsync(SubTemaDto subTema);

        // Obtener un subtema por su ID
        Task<SubTemaDto> GetSubTemaByIdAsync(int subTemaId);

        // Obtener todos los subtemas
        Task<IEnumerable<SubTemaDto>> GetAllSubTemasAsync();

        // Actualizar un subtema existente
        Task<SubTemaDto> UpdateSubTemaAsync(SubTemaDto subTema);

        // Eliminar un subtema por su ID
        Task<bool> DeleteSubTemaAsync(int subTemaId);

        // Buscar subtemas por nombre
        Task<IEnumerable<SubTemaDto>> SearchSubTemasByNameAsync(string nombreSubTema);

        // Obtener subtemas por tema
        Task<IEnumerable<SubTemaDto>> GetSubTemasByTemaAsync(int temaId);
    }
}










