using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class ExamenesHabilitadosService : IExamenesHabilitadosService
    {
        private readonly ACCDbContext _dbContext;

        public ExamenesHabilitadosService(ACCDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // metodo para: Verificar si el examen de un submódulo está habilitado para el usuario.
        public async Task<bool> VerificarExamenHabilitadoAsync(string usuarioId, int subModuloId)
        {
            var examen = await _dbContext.ExamenesHabilitados
                .FirstOrDefaultAsync(e => e.UsuarioId == usuarioId && e.Id_SubModulo == subModuloId);

            return examen?.Habilitado ?? false;
        }

        public async Task HabilitarExamenParaUsuarioAsync(string usuarioId, int subModuloId)
        {
            // Obtener todos los temas que pertenecen al submódulo
            var temasEnSubModulo = await _dbContext.Temas
                .Where(t => t.Id_SubModulo == subModuloId)
                .Select(t => t.Id_Tema)
                .ToListAsync();

            if (!temasEnSubModulo.Any())
            {
                Console.WriteLine($"No se encontraron temas en el submódulo {subModuloId}");
                return;
            }

            // Contar todos los subtemas dentro de los temas de este submódulo
            var totalSubtemas = await _dbContext.SubTemas
                .Where(st => temasEnSubModulo.Contains(st.Id_Tema))
                .CountAsync();

            // Contar los subtemas completados por el usuario en este submódulo
            var subtemasCompletados = await _dbContext.ProgresoUsuarios
                .Where(p => p.UsuarioId == usuarioId && temasEnSubModulo.Contains(p.SubTema.Id_Tema) && p.Completado)
                .CountAsync();

            // Si el usuario ha completado todos los subtemas dentro del submódulo, habilitar el examen
            if (subtemasCompletados >= totalSubtemas && totalSubtemas > 0)
            {
                var examenHabilitado = await _dbContext.ExamenesHabilitados
                    .FirstOrDefaultAsync(e => e.UsuarioId == usuarioId && e.Id_SubModulo == subModuloId);

                if (examenHabilitado == null)
                {
                    // Si no existe un registro, lo creamos
                    examenHabilitado = new ExamenHabilitado
                    {
                        UsuarioId = usuarioId,
                        Id_SubModulo = subModuloId,
                        Habilitado = true,
                        FechaHabilitacion = DateTime.Now
                    };
                    _dbContext.ExamenesHabilitados.Add(examenHabilitado);
                }
                else
                {
                    // Si ya existe, actualizamos el estado
                    examenHabilitado.Habilitado = true;
                    examenHabilitado.FechaHabilitacion = DateTime.Now;
                }

                await _dbContext.SaveChangesAsync();
            }
        }

    }
}
