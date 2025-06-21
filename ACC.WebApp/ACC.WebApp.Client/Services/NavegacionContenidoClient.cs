using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ACC.WebApp.Client.Services
{
    public class NavegacionContenidoClient(HttpClient http)
    {
        // https://localhost:7059/api/NavegacionContenido
        private readonly HttpClient _http = http;
        private readonly string _baseUrl = $"{ServiceRoots.ACC_API_Url}NavegacionContenido";

        public async Task<List<NodoJerarquicoDto>> ObtenerModulosAsync()
        {
            return await _http.GetFromJsonAsync<List<NodoJerarquicoDto>>(
                $"{_baseUrl}/modulos", _jsonOptions
                ) ?? [];
        }

        public async Task<List<NodoJerarquicoDto>> ObtenerHijosAsync(TipoNodoJerarquico tipo, int id)
        {
            return await _http.GetFromJsonAsync<List<NodoJerarquicoDto>>(
                $"{_baseUrl}/hijos/{tipo}/{id}", _jsonOptions
                ) ?? [];
        }

        public async Task<NodoJerarquicoDto?> ObtenerPadreAsync(TipoNodoJerarquico tipo, int id)
        {
            return await _http.GetFromJsonAsync<NodoJerarquicoDto>(
                $"{_baseUrl}/padre/{tipo}/{id}", _jsonOptions
                ) ?? null; 
        }

        public async Task<List<NodoJerarquicoDto>> ObtenerRutaAsync(TipoNodoJerarquico tipo, int id)
        {
            return await _http.GetFromJsonAsync<List<NodoJerarquicoDto>>(
                $"{_baseUrl}/ruta/{tipo}/{id}", _jsonOptions
                ) ?? [];
        }

        // refactorizado
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        
        public async Task<LeccionDto?> ObtenerLeccionAsync(int id)
        {
            return await _http.GetFromJsonAsync<LeccionDto>(
                $"{_baseUrl}/leccion/{id}", _jsonOptions
                ) ?? null;
        }

        public async Task<CapituloDto?> ObtenerCapituloAsync(int id)
        {
            return await _http.GetFromJsonAsync<CapituloDto>(
                $"{_baseUrl}/capitulo/{id}", _jsonOptions
                ) ?? null;
        }

        public async Task RegistrarUltimaVisitaTemaAsync(int idTema)
        {
            var response = await _http.PostAsync($"{_baseUrl}/tema/{idTema}/registrar-ultima-visita", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo registrar la última visita del tema.");
        }

        //public async Task GuardarProgresoSubTemaAsync(string usuarioId, int subTemaId)
        //{
        //    var progreso = new { UsuarioId = usuarioId, SubTemaId = subTemaId };
        //    var response = await _http.PostAsJsonAsync($"{ServiceRoots.ACC_API_Url}ProgresoUsuario/guardar", progreso, _jsonOptions);
        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("No se pudo guardar el progreso del subtema.");
        //}
    }

}
