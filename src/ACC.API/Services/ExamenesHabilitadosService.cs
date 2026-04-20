using ACC.Data;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class ExamenesHabilitadosService : IExamenesHabilitadosService
    {
        private readonly IPrerrequisitosService _pr;
        private readonly ACCDbContext _db;

        public ExamenesHabilitadosService(IPrerrequisitosService pr, ACCDbContext db)
        {
            _pr = pr;
            _db = db;
        }

        public async Task<bool> VerificarExamenHabilitadoAsync(string usuarioId, int subModuloId)
        {
            var examenId = await _db.ExamenesSubModulo
                .Where(e => e.SubModuloId == subModuloId)
                .Select(e => (int?)e.Id)
                .FirstOrDefaultAsync();

            return examenId is int id
                && id > 0
                && await _pr.EstaHabilitadoAsync(usuarioId, new ExamenRef(ExamenTipo.SubModulo, id));
        }

        public Task HabilitarExamenParaUsuarioAsync(string usuarioId, int subModuloId)
            => _pr.EvaluarDesbloqueoSubmoduloAsync(usuarioId, subModuloId);
    }
}
