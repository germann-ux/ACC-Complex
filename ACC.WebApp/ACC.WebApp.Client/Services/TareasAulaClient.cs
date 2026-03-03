using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services;

public class TareasAulaClient(HttpClient http)
{
    private const string Root = ServiceRoots.ACC_API_Url;

    public async Task<ServiceResult<IReadOnlyList<TareaListadoDto>>?> GetTareasDocenteAsync(
        int aulaId,
        CancellationToken ct = default)
        => await http.GetFromJsonAsync<ServiceResult<IReadOnlyList<TareaListadoDto>>>(
            $"{Root}Tareas/aula/{aulaId}/docente", ct);

    public async Task<ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>?> GetAlumnosAsync(
        int aulaId,
        CancellationToken ct = default)
        => await http.GetFromJsonAsync<ServiceResult<IReadOnlyList<TareaAlumnoAsignableDto>>>(
            $"{Root}Tareas/aula/{aulaId}/alumnos", ct);

    public async Task<ServiceResult<TareaListadoDto>?> CrearAsync(
        int aulaId,
        TareaCreateDto dto,
        CancellationToken ct = default)
    {
        var response = await http.PostAsJsonAsync($"{Root}Tareas/aula/{aulaId}", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaListadoDto>>(cancellationToken: ct);
    }
}
