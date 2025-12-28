using System.Net.Http.Json;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Utils;

namespace ACC.WebApp.Client.Services;

public class BibliotecaClientService(HttpClient http)
{
    private readonly HttpClient _http = http;
    private readonly string _baseUrl = $"{ServiceRoots.ACC_API_Url}Biblioteca";

    /// <summary>
    /// Obtiene todos los capítulos de Biblioteca (con tags + contenidos).
    /// </summary>
    public async Task<ServiceResult<List<CapituloDto>>> ObtenerCapitulosAsync()
    {
        return await _http.GetFromJsonAsync<ServiceResult<List<CapituloDto>>>(
            $"{_baseUrl}/capitulos", Options._jsonOptions
        ) ?? ServiceResult<List<CapituloDto>>.Fail("Respuesta nula del servidor.");
    }

    /// <summary>
    /// Obtiene un capítulo por id (con tags + contenidos).
    /// </summary>
    /// <param name="idCapitulo"></param>
    public async Task<ServiceResult<CapituloDto>> ObtenerCapituloAsync(int idCapitulo)
    {
        return await _http.GetFromJsonAsync<ServiceResult<CapituloDto>>(
            $"{_baseUrl}/capitulos/{idCapitulo}", Options._jsonOptions
        ) ?? ServiceResult<CapituloDto>.Fail("Respuesta nula del servidor.");
    }

    /// <summary>
    /// Metodo para obtener contenidos de capitulos recomendados.
    /// </summary>
    /// <param name="count"></param>
    /// <param name="maxIdContenido"></param>
    /// <returns></returns>
    public async Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerRecomendadosAsync(int count = 5, int? maxIdContenido = null)
    {
        var url = $"{_baseUrl}/contenidos/recomendados?count={count}";
        if (maxIdContenido is not null)
            url += $"&maxIdContenido={maxIdContenido.Value}";

        return await _http.GetFromJsonAsync<ServiceResult<List<ContenidoCapituloDto>>>(url, Options._jsonOptions)
            ?? ServiceResult<List<ContenidoCapituloDto>>.Fail("Respuesta nula del servidor.");
    }

}
