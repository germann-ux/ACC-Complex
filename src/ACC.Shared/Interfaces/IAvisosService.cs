using ACC.Shared.DTOs;

namespace ACC.Shared.Interfaces
{
    public interface IAvisosService // interfaz para los servicios de los avisos de los docentes en las aulas
    {
        Task<List<AvisoDto>> GetAvisosAsync(int aulaId); // metodo para obtener los avisos de un aula
        Task<AvisoDto> GetAvisoAsync(int id); // metodo para obtener un aviso en especifico
        Task<AvisoDto> CreateAvisoAsync(AvisoDto aviso); // metodo para crear un aviso
        Task<AvisoDto> UpdateAvisoAsync(AvisoDto aviso); // metodo para actualizar un aviso
        Task DeleteAvisoAsync(int id); // metodo para eliminar un aviso
    }
}
