using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Utils;
using System.Net.Http.Json;
using System.Text.Json;

namespace ACC.WebApp.Client.Services; 

public class NavegacionContenidoClient(HttpClient http)
{
    private readonly HttpClient _http = http;
    private readonly string _baseUrl = $"{ServiceRoots.ACC_API_Url}NavegacionContenido";

    public Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerModulosAsync()
        => GetAsync<List<NodoJerarquicoDto>>($"{_baseUrl}/modulos");

    public Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerHijosAsync(TipoNodoJerarquico tipo, int id)
        => GetAsync<List<NodoJerarquicoDto>>($"{_baseUrl}/hijos/{tipo}/{id}");

    public Task<ServiceResult<NodoJerarquicoDto>> ObtenerPadreAsync(TipoNodoJerarquico tipo, int id)
        => GetAsync<NodoJerarquicoDto>($"{_baseUrl}/padre/{tipo}/{id}");

    public Task<ServiceResult<List<NodoJerarquicoDto>>> ObtenerRutaAsync(TipoNodoJerarquico tipo, int id)
        => GetAsync<List<NodoJerarquicoDto>>($"{_baseUrl}/ruta/{tipo}/{id}");

    public Task<ServiceResult<LeccionDto>> ObtenerLeccionAsync(int id)
        => GetAsync<LeccionDto>($"{_baseUrl}/leccion/{id}");

    public Task<ServiceResult<CapituloDto>> ObtenerCapituloAsync(int id)
        => GetAsync<CapituloDto>($"{_baseUrl}/capitulo/{id}");

    public async Task<ServiceResult> RegistrarUltimaVisitaTemaAsync(int idTema)
    {
        try
        {
            using var response = await _http.PostAsync($"{_baseUrl}/tema/{idTema}/registrar-ultima-visita", null);
            return await ReadServiceResultAsync(response);
        }
        catch (Exception ex)
        {
            return ServiceResult.Error(ex);
        }
    }

    private async Task<ServiceResult<T>> GetAsync<T>(string url)
    {
        try
        {
            using var response = await _http.GetAsync(url);
            return await ReadServiceResultAsync<T>(response);
        }
        catch (Exception ex)
        {
            return ServiceResult<T>.Error(ex);
        }
    }

    private static async Task<ServiceResult<T>> ReadServiceResultAsync<T>(HttpResponseMessage response)
    {
        try
        {
            var result = await response.Content.ReadFromJsonAsync<ServiceResult<T>>(Options._jsonOptions);
            if (result is not null)
                return result;
        }
        catch (JsonException)
        {
        }
        catch (NotSupportedException)
        {
        }

        return new ServiceResult<T>
        {
            Success = false,
            Message = "Respuesta nula del servidor.",
            StatusCode = (int)response.StatusCode
        };
    }

    private static async Task<ServiceResult> ReadServiceResultAsync(HttpResponseMessage response)
    {
        try
        {
            var result = await response.Content.ReadFromJsonAsync<ServiceResult>(Options._jsonOptions);
            if (result is not null)
                return result;
        }
        catch (JsonException)
        {
        }
        catch (NotSupportedException)
        {
        }

        return new ServiceResult
        {
            Success = false,
            Message = "Respuesta nula del servidor.",
            StatusCode = (int)response.StatusCode
        };
    }
}
