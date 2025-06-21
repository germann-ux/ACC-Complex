using System.Collections.Generic;
using System.Threading.Tasks;
using ACC.Shared.DTOs;

namespace ACC.Shared.Interfaces
{
    public interface ITagService
    {
        // metodo para obtener todos los tags disponibles
        Task<IEnumerable<TagDto>> GetAllTagsAsync();

        // metodo para obtener los tags por su id
        Task<TagDto> GetTagByIdAsync(int id);

        // metodo para crear un nuevo tag
        Task<TagDto> CreateTagAsync(TagDto tag);

        // metodo para actualizar un tag disponible
        Task<bool> UpdateTagAsync(TagDto tag);

        // metodo para borrar un tag por su id
        Task<bool> DeleteTagAsync(int id);

        // metodo para obtener los tags de un modulo
        Task<IEnumerable<TagDto>> GetTagsByModuloIdAsync(int moduloId);

        // metodo para obtener los tags simplificados para la biblioteca
        Task<List<TagDto>> GetTagsAsync();
    }
}
