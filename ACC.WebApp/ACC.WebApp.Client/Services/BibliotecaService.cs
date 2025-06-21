using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;

namespace ACC.WebApp.Client.Services
{
    public class BibliotecaService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = $"{ServiceRoots.ACC_API_Url}";

        public BibliotecaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerContenidosAsync()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<ServiceResult<List<ContenidoCapituloDto>>>($"{BaseUrl}Biblioteca/contenidos");
                return response ?? ServiceResult<List<ContenidoCapituloDto>>.Fail("No se pudo obtener la lista de contenidos.");
            }
            catch (Exception ex)
            {
                return ServiceResult<List<ContenidoCapituloDto>>.Error(ex);
            }
        }

        public async Task<ServiceResult<ContenidoCapituloDto>> ObtenerCapituloAsync(int Id)
        {
            try
            {
                var Capitulo = await _http.GetFromJsonAsync<ServiceResult<ContenidoCapituloDto>>($"{BaseUrl}Biblioteca/Capitulo/{Id}");
                return Capitulo ?? ServiceResult<ContenidoCapituloDto>.Fail("No se pudo obtener el capitulo"); 
            }
            catch (Exception ex)
            {
                return ServiceResult<ContenidoCapituloDto>.Error(ex); 
            }
        }
    }

}
