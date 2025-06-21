using ACC.Shared.Core;
using ACC.Shared.DTOs;

namespace ACC.Shared.Interfaces
{
    public interface ITareaService
    {
        /*--- primero las tareas personales ---*/
        // crear una tarea personal
        Task<ServiceResult<TareaPersonalDto>> CreateTareaPersonalAsync(TareaPersonalDto tareaPersonal, string IdUsuario);
        // borrar una tarea personal por el id del usuario
        Task<ServiceResult<bool>> DeleteTareaPersonalAsync(int tareaPersonalId, string UserId);
        // obtener una tarea personal por el id de la tarea y el id del usuario
        Task<ServiceResult<TareaPersonalDto>> GetTareaPersonalByUserAsync(int IdTareaPersonal,string userId);
        // actualizar una tarea personal
        Task<ServiceResult<TareaPersonalDto>> UpdateTareaPersonalAsync(TareaPersonalDto tareaPersonal);
        // obtener todas las tareas personales de un usuario, se obtiene una lista
        Task<ServiceResult<List<TareaPersonalDto>>> GetTareasPersonalesByUserAsync(string userId);

        /*--- luego las tareas asignadas por los docentes en las aulas ---*/
        // creacion de una tarea asignada por un docente en un aula
        Task<ServiceResult<TareaAsignadaDto>> CreateTareaAsignadaAsync(TareaAsignadaDto tareaAsignada);
        // borrar una tarea asignada por el id de la tarea
        Task<ServiceResult<bool>> DeleteTareaAsignadaAsync(int tareaAsignadaId);
        // actualizar una tarea asignada
        Task<ServiceResult<TareaAsignadaDto>> UpdateTareaAsignadaAsync(TareaAsignadaDto tareaAsignada);
        // obtener todas las tareas asignadas de un aula, se obtiene una lista
        Task<ServiceResult<List<TareaAsignadaDto>>> GetTareasAsignadasByAulaAsync(int UserId);
        // obtener todas las tareas asignadas de un usuario por medio de su id
        Task<ServiceResult<List<TareaAsignadaDto>>> GetTareasAsignadasByUserAsync(string userId);
    }
}
