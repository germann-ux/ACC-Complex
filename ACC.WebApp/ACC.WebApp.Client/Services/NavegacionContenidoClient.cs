using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Utils;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACC.WebApp.Client.Services; 

public class NavegacionContenidoClient(HttpClient http)
{
    private readonly HttpClient _http = http;
    private readonly string _baseUrl = $"{ServiceRoots.ACC_API_Url}NavegacionContenido";

    public async Task<List<NodoJerarquicoDto>> ObtenerModulosAsync()
    {
        return await _http.GetFromJsonAsync<List<NodoJerarquicoDto>>(
            $"{_baseUrl}/modulos", Options._jsonOptions
            ) ?? [];
    }

    public async Task<List<NodoJerarquicoDto>> ObtenerHijosAsync(TipoNodoJerarquico tipo, int id)
    {
        return await _http.GetFromJsonAsync<List<NodoJerarquicoDto>>(
            $"{_baseUrl}/hijos/{tipo}/{id}", Options._jsonOptions
            ) ?? [];
    }

    public async Task<NodoJerarquicoDto?> ObtenerPadreAsync(TipoNodoJerarquico tipo, int id)
    {
        return await _http.GetFromJsonAsync<NodoJerarquicoDto>(
            $"{_baseUrl}/padre/{tipo}/{id}", Options._jsonOptions
            ) ?? null; 
    }

    public async Task<List<NodoJerarquicoDto>> ObtenerRutaAsync(TipoNodoJerarquico tipo, int id)
    {
        return await _http.GetFromJsonAsync<List<NodoJerarquicoDto>>(
            $"{_baseUrl}/ruta/{tipo}/{id}", Options._jsonOptions
            ) ?? [];
    }
    
    public async Task<LeccionDto?> ObtenerLeccionAsync(int id)
    {
        return await _http.GetFromJsonAsync<LeccionDto>(
            $"{_baseUrl}/leccion/{id}", Options._jsonOptions
            ) ?? null;
    }

    public async Task<CapituloDto?> ObtenerCapituloAsync(int id)
    {
        return await _http.GetFromJsonAsync<CapituloDto>(
            $"{_baseUrl}/capitulo/{id}", Options._jsonOptions
            ) ?? null;
    }

    public async Task RegistrarUltimaVisitaTemaAsync(int idTema)
    {
        var response = await _http.PostAsync($"{_baseUrl}/tema/{idTema}/registrar-ultima-visita", null);
        if (!response.IsSuccessStatusCode)
            throw new Exception("No se pudo registrar la última visita del tema.");
    }
}
