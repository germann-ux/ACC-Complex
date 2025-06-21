using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ACC.API.Services
{
    public class ProgresoUsuarioService : IProgresoUsuarioService
    {
        private readonly ACCDbContext _dbContext;
        private readonly IExamenesHabilitadosService _examenesHabilitadosService;

        public ProgresoUsuarioService(ACCDbContext dbContext, IExamenesHabilitadosService examenesHabilitadosService)
        {
            _dbContext = dbContext;
            _examenesHabilitadosService = examenesHabilitadosService;
        }

        public async Task GuardarProgresoAsync(string usuarioId, int SubTemaId)
        {
            if (string.IsNullOrEmpty(usuarioId))
            {
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(usuarioId));
            }

            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == SubTemaId);

            if (progreso == null)
            {
                // Crear un nuevo registro si no existe
                progreso = new ProgresoUsuario
                {
                    UsuarioId = usuarioId,
                    SubTemaId = SubTemaId,
                    Fecha = DateTime.Now
                };
                await _dbContext.ProgresoUsuarios.AddAsync(progreso);
            }
            else
            {
                // Actualizar el registro existente
                progreso.Fecha = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task<int?> ObtenerUltimoTemaAsync(string usuarioId)
        {
            var progreso = await _dbContext.ProgresoUsuarios
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Fecha) // Ordena por fecha más reciente
                .FirstOrDefaultAsync(); // Obtén el primer registro más reciente

            return progreso?.SubTemaId;
        }

        // metodo para marcar un subtema como visto
        public async Task MarcarSubtemaComoCompletadoAsync(string usuarioId, int subTemaId)
        {
            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == subTemaId);

            if (progreso == null)
            {
                progreso = new ProgresoUsuario
                {
                    UsuarioId = usuarioId,
                    SubTemaId = subTemaId,
                    Fecha = DateTime.Now,
                    Completado = true
                };
                _dbContext.ProgresoUsuarios.Add(progreso);
            }
            else
            {
                progreso.Completado = true;
                progreso.Fecha = DateTime.Now;
            }

            await _dbContext.SaveChangesAsync();

            // 🔹 Obtener el subtema para acceder a su tema relacionado
            var subTema = await _dbContext.SubTemas
                .Include(st => st.Tema) // Incluir la relación con Tema
                .FirstOrDefaultAsync(st => st.Id_SubTema == subTemaId);

            if (subTema?.Tema != null)
            {
                // 🔹 Obtener el ID del submódulo desde el tema asociado
                int subModuloId = subTema.Tema.Id_SubModulo;

                // 🔹 Verificar si el examen debe habilitarse
                await _examenesHabilitadosService.HabilitarExamenParaUsuarioAsync(usuarioId, subModuloId);
            }
        }

        public async Task<bool> EstaSubtemaCompletadoAsync(string usuarioId, int subTemaId)
        {
            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == subTemaId);

            return progreso?.Completado ?? false;
        }
    }
}
