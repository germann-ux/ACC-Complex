using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services;

public class TareasPersonalesClient(HttpClient http)
{
    private const string Root = ServiceRoots.ACC_API_Url;

    public async Task<ServiceResult<List<TareaPersonalDto>>?> GetByUserAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<ServiceResult<List<TareaPersonalDto>>>(
            $"{Root}Tarea/personal/lista", ct);

    public async Task<ServiceResult<TareaPersonalDto>?> CreateAsync(
        TareaPersonalDto dto,
        CancellationToken ct = default)
    {
        var response = await http.PostAsJsonAsync($"{Root}Tarea/personal", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaPersonalDto>>(cancellationToken: ct);
    }

    public async Task<ServiceResult<TareaPersonalDto>?> UpdateAsync(
        TareaPersonalDto dto,
        CancellationToken ct = default)
    {
        var response = await http.PutAsJsonAsync($"{Root}Tarea/personal", dto, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<TareaPersonalDto>>(cancellationToken: ct);
    }

    public async Task<ServiceResult<bool>?> DeleteAsync(
        int tareaPersonalId,
        CancellationToken ct = default)
    {
        var response = await http.DeleteAsync($"{Root}Tarea/personal/{tareaPersonalId}", ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<bool>>(cancellationToken: ct);
    }
}
