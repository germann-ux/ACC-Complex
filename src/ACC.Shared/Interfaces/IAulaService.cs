using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface IAulaService //TODO: me falta arreglar todo el sistema de los avisos con las tareas las aulas y etc con los usuarios docentes
    {
        // Crear un nuevo aula
        Task<ServiceResult<AulaDto>> CreateAulaAsync(AulaDto Aula);

        // Obtener un aula por su ID
        Task<ServiceResult<AulaDto>> GetAulaByIdAsync(int AulaId);

        // Actualizar un aula existente
        Task<ServiceResult<AulaDto>> UpdateAulaAsync(AulaDto Aula);

        // Eliminar un aula por su ID
        Task<bool> DeleteAulaAsync(int AulaId);

        // Obtener aulas por módulo
        Task<ServiceResult<IEnumerable<AulaDto>>> GetAulasByModuloAsync(int ModuloId);

        Task<string> GetClaveAula(int IdDocente); // Generar clave de aula

        /*Vista de docentes*/
        // metodo para crear un aviso y guardarlo en la base de datos.
        Task<ServiceResult<AvisoDto>> CreateAvisoAsync(AvisoDto Aviso); 
        // metodo para actualizar un aviso y guardar los cambios en la base de datos.
        Task<ServiceResult<AvisoDto>> UpdateAvisoAsync(AvisoDto Aviso);
        // metodo para crear una tarea y asignarla a un aula.
        Task<ServiceResult<TareaAsignadaDto>> AsignarTareaAulaAsync(TareaAsignadaDto Tarea, int IdAula);
        //metodo para eliminar a un alumno de un aula.
        Task<ServiceResult<bool>> EliminarAulmnoAulaAsync(); 

        /*Vista de estudiantes*/
        // metodo para recibir una tarea del alumno y guardarla como completada.
        Task<ServiceResult<bool>> MarcarComoCompletadaAsync(int IdTarea, string UserId);
        // Por ahora es todo lo nesesario.
    }
}


