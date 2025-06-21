namespace ACC.Shared.Interfaces
{
    // Interfaz que define el contrato para el servicio de estado de roles
    public interface IRoleStateService
    {
        // Propiedad que indica si el usuario ha visitado la página de verificación
        bool HasVisitedVerificationPage { get; set; }

        // Propiedad que almacena el rol del usuario
        string UserRole { get; set; }

        // Propiedad que indica si el usuario ha cerrado la alerta
        bool HasClosedAlert { get; set; }

        // Evento que se dispara cuando cambia el estado de la alerta
        event Action OnAlertStateChanged;

        // Método para inicializar el servicio de manera asíncrona
        Task InitializeAsync();

        // Método para establecer el rol del usuario de manera asíncrona
        Task SetUserRoleAsync(string role);

        // Método para marcar la alerta como cerrada de manera asíncrona
        Task CloseAlertAsync();

        // Método para obtener el estado de la alerta de manera asíncrona
        Task EstadoAviso();
    }
}
