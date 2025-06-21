using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services //TODO: esta madre se ocupa arreglar xddd, no tiene sentido alguno, hasta coraje me dio
{
    public class HistorialCalificacionesService : IHistorialCalificacionesService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _Mapper; 

        public HistorialCalificacionesService(ACCDbContext context, IMapper mapper)
        {
            _Mapper = mapper;
            _context = context;
        }

        public async Task<HistorialCalificacionesDto> CreateHistorialAsync(HistorialCalificacionesDto historial)
        {
            var historialEntity = _Mapper.Map<HistorialCalificaciones>(historial);
            _context.HistorialCalificaciones.Add(historialEntity);
            await _context.SaveChangesAsync();
            return historial;
        }

        public async Task<HistorialCalificacionesDto> GetHistorialByIdAsync(int historialId)
        {
            var HistorialEntity = _Mapper.Map<HistorialCalificacionesDto>(await _context.HistorialCalificaciones
                .Include(h => h.Usuario)
                .Include(h => h.Modulo)
                .Include(h => h.SubModulo)
                .FirstOrDefaultAsync(h => h.Id_Historial == historialId));
            return HistorialEntity;
        }

        public async Task<IEnumerable<HistorialCalificacionesDto>> GetAllHistorialesAsync()
        {
            var TodosLosHistoriales = _Mapper.Map<IEnumerable<HistorialCalificacionesDto>>(await _context.HistorialCalificaciones
                .Include(h => h.Usuario)
                .Include(h => h.Modulo)
                .Include(h => h.SubModulo)
                .ToListAsync());
            return TodosLosHistoriales;
        }

        public async Task<HistorialCalificacionesDto> UpdateHistorialAsync(HistorialCalificacionesDto historial)
        {
            var historialEntity = await _context.HistorialCalificaciones.FindAsync(historial.Id_Historial);
            _context.HistorialCalificaciones.Update(historialEntity);
            await _context.SaveChangesAsync();
            return historial;
        }

        public async Task<bool> DeleteHistorialAsync(int historialId)
        {
            var historial = await _context.HistorialCalificaciones.FindAsync(historialId);
            if (historial == null)
            {
                return false;
            }

            _context.HistorialCalificaciones.Remove(historial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<HistorialCalificacionesDto>> GetHistorialesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Obtiene la calificación promedio de un módulo para un usuario.
        /// </summary>
        public async Task<decimal?> GetCalificacionModuloAsync(string userId, int moduloId)
        {
            var calificaciones = await _context.HistorialCalificaciones
                .Where(h => h.Id_Usuario == userId && h.Id_Modulo == moduloId)
                .Select(h => h.Calificacion)
                .ToListAsync();

            return calificaciones.Any() ? calificaciones.Average() : null;
        }

        /// <summary>
        /// Obtiene la calificación de un submódulo para un usuario.
        /// </summary>
        public async Task<decimal?> GetCalificacionSubModuloAsync(string userId, int subModuloId)
        {
            return await _context.HistorialCalificaciones
                .Where(h => h.Id_Usuario == userId && h.Id_SubModulo == subModuloId)
                .Select(h => h.Calificacion)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Registra o actualiza la calificación de un módulo para un usuario.
        /// </summary>
        public async Task<bool> PostCalificacionModuloAsync(string userId, int moduloId, decimal calificacion)
        {
            var historial = await _context.HistorialCalificaciones
                .FirstOrDefaultAsync(h => h.Id_Usuario == userId && h.Id_Modulo == moduloId);

            if (historial == null)
            {
                historial = new HistorialCalificaciones
                {
                    Id_Usuario = userId,
                    Id_Modulo = moduloId,
                    Calificacion = calificacion,
                    FechaCalificacion = DateTime.Now
                };
                _context.HistorialCalificaciones.Add(historial);
            }
            else
            {
                historial.Calificacion = calificacion;
                historial.FechaCalificacion = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Registra o actualiza la calificación de un submódulo para un usuario.
        /// </summary>
        public async Task<bool> PostCalificacionSubModuloAsync(string userId, int subModuloId, decimal calificacion)
        {
            var historial = await _context.HistorialCalificaciones
                .FirstOrDefaultAsync(h => h.Id_Usuario == userId && h.Id_SubModulo == subModuloId);

            if (historial == null)
            {
                historial = new HistorialCalificaciones
                {
                    Id_Usuario = userId,
                    Id_SubModulo = subModuloId,
                    Calificacion = calificacion,
                    FechaCalificacion = DateTime.Now
                };
                _context.HistorialCalificaciones.Add(historial);
            }
            else
            {
                historial.Calificacion = calificacion;
                historial.FechaCalificacion = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}




