using ACC.Shared.Interfaces;
using Blazored.LocalStorage;

namespace ACC.WebApp.Services
{
    public class RoleStateService : IRoleStateService
    {
        private readonly ILocalStorageService _localStorage;

        public RoleStateService(ILocalStorageService localStorage) => _localStorage = localStorage;

        public bool HasVisitedVerificationPage { get; set; }
        public string UserRole { get; set; } = "Estudiante"; // Por defecto, el rol es "Estudiante"
        public bool HasClosedAlert { get; set; } // Nueva propiedad para indicar si el usuario ha cerrado el aviso

        public event Action OnAlertStateChanged;

        public async Task InitializeAsync()
        {
            HasClosedAlert = await _localStorage.GetItemAsync<bool>("hasClosedAlert");
            OnAlertStateChanged?.Invoke();
        }

        public Task SetUserRoleAsync(string role)
        {
            UserRole = role;
            return Task.CompletedTask;
        }

        public async Task CloseAlertAsync()
        {
            HasClosedAlert = true;
            await _localStorage.SetItemAsync("hasClosedAlert", true);
            OnAlertStateChanged?.Invoke();
        }

        public async Task EstadoAviso()
        {
            HasClosedAlert = await _localStorage.GetItemAsync<bool>("hasClosedAlert");
            OnAlertStateChanged?.Invoke();
        }
    }
}
