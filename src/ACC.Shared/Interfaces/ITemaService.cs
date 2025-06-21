using ACC.Shared.DTOs;

namespace ACC.Shared.Interfaces
{
    public interface ITemaService
    {
        // Crear un nuevo tema
        Task<TemaDto> CreateTemaAsync(TemaDto tema);

        // Obtener un tema por su ID
        Task<TemaDto> GetTemaByIdAsync(int temaId);

        // Obtener todos los temas
        Task<IEnumerable<TemaDto>> GetAllTemasAsync();

        // Actualizar un tema existente
        Task<TemaDto> UpdateTemaAsync(TemaDto tema);

        // Eliminar un tema por su ID
        Task<bool> DeleteTemaAsync(int temaId);

        // Buscar temas por nombre
        Task<IEnumerable<TemaDto>> SearchTemasByNameAsync(string nombreTema);

        // obtener temas por submodulo
        Task<IEnumerable<TemaDto>> GetTemasBySubModuloAsync(int subModuloId);

        // obtener el progreso de un tema por el id del usuario 
        Task<UsuarioTemasDto> GetTemaProgresAsync(string idUsuario);

        // obtener los temas por submodulo
        Task<List<TemaDto>> GetTemasPorSubModuloAsync(int idSubModulo);

        // Obtener la ultima visita de un tema por el id del usuario
        Task<UsuarioTemasDto> GetUltimaVisitaTemaAsync(string idUsuario, int idTema);
    }
}
