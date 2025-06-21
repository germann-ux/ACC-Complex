using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface IHistorialCalificacionesService
    {
        // Crear un nuevo registro de historial de calificaciones
        Task<HistorialCalificacionesDto> CreateHistorialAsync(HistorialCalificacionesDto historial);

        // Obtener un registro de historial de calificaciones por su ID
        Task<HistorialCalificacionesDto> GetHistorialByIdAsync(int historialId);

        // Obtener todos los registros de historial de calificaciones
        Task<IEnumerable<HistorialCalificacionesDto>> GetAllHistorialesAsync();

        // Actualizar un registro de historial de calificaciones existente
        Task<HistorialCalificacionesDto> UpdateHistorialAsync(HistorialCalificacionesDto historial);

        // Eliminar un registro de historial de calificaciones por su ID
        Task<bool> DeleteHistorialAsync(int historialId);

        // Buscar registros de historial de calificaciones por usuario
        Task<IEnumerable<HistorialCalificacionesDto>> GetHistorialesByUserIdAsync(string userId);

        Task<decimal?> GetCalificacionModuloAsync(string userId, int moduloId);

        Task<decimal?> GetCalificacionSubModuloAsync(string userId, int subModuloId);

        Task<bool> PostCalificacionModuloAsync(string userId, int moduloId, decimal calificacion);

        Task<bool> PostCalificacionSubModuloAsync(string userId, int subModuloId, decimal calificacion);
    }
}



