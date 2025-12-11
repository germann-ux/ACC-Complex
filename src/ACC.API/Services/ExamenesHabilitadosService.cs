using ACC.Shared.Enums;
using ACC.Shared.Interfaces;

namespace ACC.API.Services
{
    public class ExamenesHabilitadosService : IExamenesHabilitadosService
    {
        private readonly IPrerrequisitosService _pr;

        public ExamenesHabilitadosService(IPrerrequisitosService pr) => _pr = pr;

        public Task<bool> VerificarExamenHabilitadoAsync(string usuarioId, int subModuloId)
            => _pr.EstaHabilitadoAsync(usuarioId, new ExamenRef(ExamenTipo.SubModulo, subModuloId));

        public Task HabilitarExamenParaUsuarioAsync(string usuarioId, int subModuloId)
            => _pr.EvaluarDesbloqueoSubmoduloAsync(usuarioId, subModuloId); // ← ojo: NO por subTema
    }
}
