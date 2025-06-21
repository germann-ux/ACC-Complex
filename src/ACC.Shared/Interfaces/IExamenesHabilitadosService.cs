namespace ACC.Shared.Interfaces
{
    public interface IExamenesHabilitadosService
    {
        /// <summary>
        /// Habilita el examen del submódulo si el usuario ha completado todos los subtemas.
        /// </summary>
        Task HabilitarExamenParaUsuarioAsync(string usuarioId, int subModuloId);

        /// <summary>
        /// Verifica si el examen de un submódulo está habilitado para el usuario.
        /// </summary>
        Task<bool> VerificarExamenHabilitadoAsync(string usuarioId, int subModuloId);
    }
}
