using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace ACC.WebApp.Client.Services;

public class ProgresoUsuarioClient(HttpClient http)
{
    private readonly HttpClient _http = http;
    private readonly string _baseUrl = "ProgresoUsuario";

    public record ExamenHabilitadoDto(bool ExamenHabilitado);

    public async Task<bool> ExamenHabilitadoAsync(string userId, ExamenTipo tipo, int refId)
    {
        if (string.IsNullOrWhiteSpace(userId)) return false;
        if (refId <= 0) return false;

        var url = $"{_baseUrl}/examen-habilitado/{userId}/{tipo}/{refId}";
        var result = await _http.GetFromJsonAsync<ExamenHabilitadoDto>(url, Options._jsonOptions);
        return result?.ExamenHabilitado ?? false;
    }

    public Task<bool> ExamenSubModuloHabilitadoAsync(string userId, int subModuloId)
        => ExamenHabilitadoAsync(userId, ExamenTipo.SubModulo, subModuloId);

    public Task<bool> ExamenModuloHabilitadoAsync(string userId, int moduloId)
        => ExamenHabilitadoAsync(userId, ExamenTipo.Modulo, moduloId);

    public Task<bool> ExamenLibreHabilitadoAsync(string userId, int examenId)
        => ExamenHabilitadoAsync(userId, ExamenTipo.Libre, examenId);

    public async Task GuardarProgresoSubTemaAsync(string usuarioId, int subTemaId)
    {
        var url = $"{_baseUrl}/guardar";
        var body = new ProgresoUsuarioDto { UsuarioId = usuarioId, SubTemaId = subTemaId };

        var resp = await _http.PostAsJsonAsync(url, body, Options._jsonOptions);
        var content = await resp.Content.ReadAsStringAsync();

        if (!resp.IsSuccessStatusCode)
            throw new Exception($"POST {url} -> {(int)resp.StatusCode} {resp.ReasonPhrase}. Body: {content}");
    }

    public async Task<bool> MarcarSubtemaComoCompletadoAsync(string userId, int subTemaId)
    {
        var url = $"{_baseUrl}/completar";
        var body = new ProgresoUsuarioDto { UsuarioId = userId, SubTemaId = subTemaId };

        var resp = await _http.PostAsJsonAsync(url, body, Options._jsonOptions);
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> ObtenerEstadoSubtema(string usuarioId, int subTemaId)
    {
        var url = $"{_baseUrl}/subtema-completado/{usuarioId}/{subTemaId}";
        try
        {
            var resp = await _http.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return false;

            var json = await resp.Content.ReadFromJsonAsync<JsonElement>(Options._jsonOptions);
            if (json.TryGetProperty("Completado", out var v1)) return v1.GetBoolean();
            if (json.TryGetProperty("completado", out var v2)) return v2.GetBoolean();
        }
        catch
        {
        }

        return false;
    }
}
