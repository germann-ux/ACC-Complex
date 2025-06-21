namespace ACC.Shared.Interfaces
{
    public interface IProgresoUsuarioService
    {
        /// <summary>
        /// Guarda el progreso del usuario con el ID del tema actual.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <param name="temaId">ID del tema visto.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        Task GuardarProgresoAsync(string usuarioId, int temaId);

        /// <summary>
        /// Obtiene el ID del último tema visto por el usuario.
        /// </summary>
        /// <param name="usuarioId">ID del usuario.</param>
        /// <returns>El ID del último tema visto, o null si no hay progreso registrado.</returns>
        Task<int?> ObtenerUltimoTemaAsync(string usuarioId);

        //metodo para marcar un tema como visto
        Task MarcarSubtemaComoCompletadoAsync(string usuarioId, int SubtemaId);

        /// <summary>
        /// Metodo para obtener si un subtema esta completado para el usuario.
        /// </summary>
        Task<bool> EstaSubtemaCompletadoAsync(string usuarioId, int subTemaId); 
    }
}
