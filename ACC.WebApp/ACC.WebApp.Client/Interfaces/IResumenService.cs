using ACC.Shared.DTOs;

namespace ACC.WebApp.Client.Interfaces
{
    public interface IResumenService
    {
        Task<ApplicationUserDto?> GetUsuarioAsync(string userId, CancellationToken ct);
        Task<(TareasPendientesResumenDto? resumen, List<TareaAlumnoListadoDto> asignadas, List<TareaPersonalDto> personales)> GetTareasAsync(string userId, CancellationToken ct);
        Task<TipDto?> GetTipAsync(CancellationToken ct);
        Task<int?> GetUltimoTemaIdAsync(string userId, CancellationToken ct);
        Task<List<NotaTiempoDto>> GetNotasTiempoAsync(string userId, CancellationToken ct);
    }
}
