using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Utils;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services;

public sealed class LeccionesAdminClientService(HttpClient http)
{
    private const string BaseUrl = "admin/lecciones";

    public async Task<ServiceResult<List<LeccionAdminDto>>?> ListarAsync(int? subTemaId, CancellationToken ct = default)
    {
        var url = subTemaId.HasValue ? $"{BaseUrl}?subTemaId={subTemaId.Value}" : BaseUrl;
        return await http.GetFromJsonAsync<ServiceResult<List<LeccionAdminDto>>>(url, Options._jsonOptions, ct);
    }

    public Task<ServiceResult<LeccionAdminDto>?> ObtenerAsync(int idLeccion, CancellationToken ct = default)
        => http.GetFromJsonAsync<ServiceResult<LeccionAdminDto>>($"{BaseUrl}/{idLeccion}", Options._jsonOptions, ct);

    public async Task<ServiceResult<LeccionAdminDto>?> CrearAsync(LeccionAdminDto dto, CancellationToken ct = default)
    {
        var response = await http.PostAsJsonAsync(BaseUrl, dto, Options._jsonOptions, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<LeccionAdminDto>>(Options._jsonOptions, ct);
    }

    public async Task<ServiceResult<LeccionAdminDto>?> ActualizarAsync(int idLeccion, LeccionAdminDto dto, CancellationToken ct = default)
    {
        var response = await http.PutAsJsonAsync($"{BaseUrl}/{idLeccion}", dto, Options._jsonOptions, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult<LeccionAdminDto>>(Options._jsonOptions, ct);
    }

    public async Task<ServiceResult?> PublicarAsync(int idLeccion, CancellationToken ct = default)
    {
        var response = await http.PostAsync($"{BaseUrl}/{idLeccion}/publicar", null, ct);
        return await response.Content.ReadFromJsonAsync<ServiceResult>(Options._jsonOptions, ct);
    }
}

