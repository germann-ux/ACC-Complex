using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACC.WebApp.Client.Services;

public sealed class AgendaClientService(HttpClient http)
{
    private const string Root = ServiceRoots.ACC_API_Url;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<AgendaOverviewDto> GetOverviewAsync(CancellationToken ct = default)
    {
        var personalesTask = GetPersonalesAsync(ct);
        var pendientesTask = GetPendientesAsync(ct);
        var asignadasTask = GetAsignadasAsync(ct);

        await Task.WhenAll(personalesTask!, pendientesTask!, asignadasTask!);

        var personalesResponse = personalesTask.Result;
        var pendientesResponse = pendientesTask.Result;
        var asignadasResponse = asignadasTask.Result;

        return new AgendaOverviewDto(
            Personales: personalesResponse?.Data ?? [],
            Asignadas: asignadasResponse?.Data ?? [],
            Pendientes: pendientesResponse?.Data,
            ErrorMessage: PrimerError(personalesResponse?.Message, pendientesResponse?.Message, asignadasResponse?.Message));
    }

    public async Task<ServiceResult<List<TareaPersonalDto>>?> GetPersonalesAsync(CancellationToken ct = default)
        => await GetWithNotFoundAsEmptyAsync<List<TareaPersonalDto>>($"{Root}Tarea/personal/lista", ct);

    public async Task<ServiceResult<TareasPendientesResumenDto>?> GetPendientesAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<ServiceResult<TareasPendientesResumenDto>>(
            $"{Root}TareasAlumno/resumen",
            JsonOptions,
            ct);

    public async Task<ServiceResult<List<TareaAlumnoListadoDto>>?> GetAsignadasAsync(CancellationToken ct = default)
        => await GetWithNotFoundAsEmptyAsync<List<TareaAlumnoListadoDto>>($"{Root}TareasAlumno/listado", ct);

    public async Task<ServiceResult<TareaPersonalDto>?> CreatePersonalAsync(TareaPersonalDto dto, CancellationToken ct = default)
    {
        var response = await http.PostAsJsonAsync($"{Root}Tarea/personal", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaPersonalDto>>(
            JsonOptions,
            cancellationToken: ct);
    }

    public async Task<ServiceResult<TareaPersonalDto>?> UpdatePersonalAsync(TareaPersonalDto dto, CancellationToken ct = default)
    {
        var response = await http.PutAsJsonAsync($"{Root}Tarea/personal", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaPersonalDto>>(
            JsonOptions,
            cancellationToken: ct);
    }

    public async Task<ServiceResult<bool>?> DeletePersonalAsync(int tareaPersonalId, CancellationToken ct = default)
    {
        var response = await http.DeleteAsync($"{Root}Tarea/personal/{tareaPersonalId}", ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<bool>>(
            JsonOptions,
            cancellationToken: ct);
    }

    private async Task<ServiceResult<T>?> GetWithNotFoundAsEmptyAsync<T>(string url, CancellationToken ct = default)
    {
        try
        {
            return await http.GetFromJsonAsync<ServiceResult<T>>(url, JsonOptions, ct);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return ServiceResult<T>.Ok(default);
        }
    }

    private static string? PrimerError(params string?[] mensajes)
    {
        foreach (var mensaje in mensajes)
        {
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                return mensaje;
            }
        }

        return null;
    }
}

public sealed record AgendaOverviewDto(
    List<TareaPersonalDto> Personales,
    List<TareaAlumnoListadoDto> Asignadas,
    TareasPendientesResumenDto? Pendientes,
    string? ErrorMessage);
