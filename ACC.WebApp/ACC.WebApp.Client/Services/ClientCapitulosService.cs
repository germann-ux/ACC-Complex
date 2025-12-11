// ACC.WebApp/Client/Services/ClientCapitulosService.cs
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services
{
    public class ClientCapitulosService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<ContenidoCapituloDto>> GetCapitulosRecomendadosAsync(List<int> seleccionados)
        {
            var response = await _httpClient.PostAsJsonAsync($"{ServiceRoots.ACC_API_CAPITULOS}recomendados", seleccionados);
            if (response.IsSuccessStatusCode)
            {
                var solicitud = await response.Content.ReadFromJsonAsync<ServiceResult<List<ContenidoCapituloDto>>>();
                return solicitud == null ? throw new Exception("La respuesta del servidor es nula.") : solicitud.Data ?? [];
            }
            throw new Exception($"Error al obtener capítulos recomendados: {response.ReasonPhrase}");
        }

        // ✅ NUEVO: backend random
        public async Task<List<ContenidoCapituloDto>> GetCapitulosRecomendadosRandomAsync(int max = 100, int count = 5)
        {
            var url = $"{ServiceRoots.ACC_API_CAPITULOS}recomendados/random?max={max}&count={count}";
            var solicitud = await _httpClient.GetFromJsonAsync<ServiceResult<List<ContenidoCapituloDto>>>(url);
            if (solicitud?.Success == true)
                return solicitud.Data ?? new();
            return []; // tolerante: UI mostrará "No tenemos recomendaciones por ahora."
        }
    }
}
