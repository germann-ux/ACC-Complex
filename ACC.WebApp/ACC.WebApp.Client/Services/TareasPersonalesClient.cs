using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services;

public class TareasPersonalesClient(HttpClient http)
{
    public async Task<ServiceResult<List<TareaPersonalDto>>?> GetByUserAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<ServiceResult<List<TareaPersonalDto>>>(
            "Tarea/personal/lista", ct);

    public async Task<ServiceResult<TareaPersonalDto>?> CreateAsync(
        TareaPersonalDto dto,
        CancellationToken ct = default)
    {
        var response = await http.PostAsJsonAsync("Tarea/personal", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaPersonalDto>>(cancellationToken: ct);
    }

    public async Task<ServiceResult<TareaPersonalDto>?> UpdateAsync(
        TareaPersonalDto dto,
        CancellationToken ct = default)
    {
        var response = await http.PutAsJsonAsync("Tarea/personal", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaPersonalDto>>(cancellationToken: ct);
    }

    public async Task<ServiceResult<bool>?> DeleteAsync(
        int tareaPersonalId,
        CancellationToken ct = default)
    {
        var response = await http.DeleteAsync($"Tarea/personal/{tareaPersonalId}", ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<bool>>(cancellationToken: ct);
    }
}

