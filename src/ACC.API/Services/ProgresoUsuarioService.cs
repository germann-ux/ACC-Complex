using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ACC.API.Services
{
    public class ProgresoUsuarioService : IProgresoUsuarioService
    {
        private readonly ACCDbContext _dbContext;
        //private readonly IExamenesHabilitadosService _examenesHabilitadosService;
        private readonly IPrerrequisitosService _prerrequisitosService; 

        public ProgresoUsuarioService(ACCDbContext dbContext, IPrerrequisitosService prerrequisitosService)
        {
            _dbContext = dbContext;
            _prerrequisitosService = prerrequisitosService;
        }

        public async Task GuardarProgresoAsync(string usuarioId, int SubTemaId)
        {
            if (string.IsNullOrEmpty(usuarioId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(usuarioId));

            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == SubTemaId);

            if (progreso == null)
            {
                progreso = new ProgresoUsuario
                {
                    UsuarioId = usuarioId,
                    SubTemaId = SubTemaId,
                    Fecha = DateTimeOffset.UtcNow
                };
                await _dbContext.ProgresoUsuarios.AddAsync(progreso);
            }
            else
            {
                progreso.Fecha = DateTimeOffset.UtcNow; // ← unifica Offset
            }

            await _dbContext.SaveChangesAsync();
        }
        // MarcarSubtemaComoCompletadoAsync

        public async Task<int?> ObtenerUltimoTemaAsync(string usuarioId)
        {
            var subTemaId = await _dbContext.ProgresoUsuarios
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Fecha)
                .Select(p => (int?)p.SubTemaId)   // ← trae solo lo que necesitas
                .FirstOrDefaultAsync();

            return subTemaId;
        }

        public async Task MarcarSubtemaComoCompletadoAsync(string usuarioId, int subTemaId)
        {
            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == subTemaId);

            if (progreso is null)
            {
                progreso = new ProgresoUsuario
                {
                    UsuarioId = usuarioId,
                    SubTemaId = subTemaId,
                    Fecha = DateTimeOffset.UtcNow,
                    Completado = true
                };
                _dbContext.ProgresoUsuarios.Add(progreso);
            }
            else
            {
                if (!progreso.Completado)
                    progreso.Completado = true;

                progreso.Fecha = DateTimeOffset.UtcNow;
            }

            var affected = await _dbContext.SaveChangesAsync();
            // Disparo idempotente: aunque affected sea 0, reevalúa y hace UPSERT si corresponde
            await _prerrequisitosService.EvaluarDesbloqueosPorProgresoAsync(usuarioId, subTemaId);
        }


        public Task<bool> EstaSubtemaCompletadoAsync(string usuarioId, int subTemaId)
        {
            return _dbContext.ProgresoUsuarios
                .AsNoTracking()
                .AnyAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == subTemaId && p.Completado);
        }

        // === NUEVO ===
        public Task<bool> ExamenHabilitadoAsync(string userId, ExamenRef examen)
            => _prerrequisitosService.EstaHabilitadoAsync(userId, examen);

        public Task<bool> ExamenSubModuloHabilitadoAsync(string userId, int subModuloId)
            => ExamenHabilitadoAsync(userId, new ExamenRef(ExamenTipo.SubModulo, subModuloId));

        public Task<bool> ExamenModuloHabilitadoAsync(string userId, int moduloId)
            => ExamenHabilitadoAsync(userId, new ExamenRef(ExamenTipo.Modulo, moduloId));

        public Task<bool> ExamenLibreHabilitadoAsync(string userId, int examenId)
            => ExamenHabilitadoAsync(userId, new ExamenRef(ExamenTipo.Libre, examenId));
    }
}
