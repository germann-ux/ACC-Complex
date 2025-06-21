using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ProgresoUsuarioClient
{
    private readonly HttpClient _http;
    private readonly string _baseUrl = $"{ServiceRoots.ACC_API_Url}ProgresoUsuario";

    public ProgresoUsuarioClient(HttpClient http)
    {
        _http = http;
    }

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<bool> ExamenHabilitadoAsync(string userId, int subModuloId)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<JsonElement>($"{_baseUrl}/examen-habilitado/{userId}/{subModuloId}");
            return result.GetProperty("examenHabilitado").GetBoolean();
        }
        catch
        {
            return false;
        }
    }

    public async Task GuardarProgresoSubTemaAsync(string usuarioId, int subTemaId)
    {
        var progreso = new { UsuarioId = usuarioId, SubTemaId = subTemaId };
        var response = await _http.PostAsJsonAsync($"{_baseUrl}/guardar", progreso, _jsonOptions);
        if (!response.IsSuccessStatusCode)
            throw new Exception("No se pudo guardar el progreso del subtema.");
    }

    public async Task<bool> MarcarSubtemaComoCompletadoAsync(string userId, int subTemaId)
    {
        var progreso = new ProgresoUsuarioDto
        {
            UsuarioId = userId,
            SubTemaId = subTemaId
        };

        var response = await _http.PostAsJsonAsync($"{_baseUrl}/completar", progreso);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ObtenerEstadoSubtema(string usuarioId, int subTemaId)
    {
        if (string.IsNullOrWhiteSpace(usuarioId))
            throw new ArgumentException("El usuarioId no puede ser nulo o vacío.", nameof(usuarioId));

        var url = $"{_baseUrl}/subtema-completado/{usuarioId}/{subTemaId}";

        try
        {
            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[ERROR] Falló la petición a {url} - Código: {response.StatusCode}");
                return false;
            }

            var result = await response.Content.ReadFromJsonAsync<JsonElement>(_jsonOptions);

            if (result.TryGetProperty("completado", out var completado))
            {
                return completado.GetBoolean();
            }

            Console.WriteLine("[WARN] No se encontró la propiedad 'completado' en la respuesta.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] Ocurrió una excepción al obtener estado del subtema: {ex.Message}");
            return false;
        }
    }

}