using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Utils;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services
{
    public class ExamenesServiceClient(HttpClient http)
    {
        private readonly HttpClient _http = http;
        private readonly string _baseUrlModExams = $"{ServiceRoots.ACC_API_EXAMENES}";

        public async Task<List<ExamenSubModuloDto>> ObtenerExamanesSubMAsync()
        {
            return await _http.GetFromJsonAsync<List<ExamenSubModuloDto>>(
                $"{_baseUrlModExams}ExamenesSubM/todos", Options._jsonOptions
                ) ?? [];
        }

        public async Task<ExamenSubModuloDto?> ObtenerExamenSubMAsync(int id)
        {
            return await _http.GetFromJsonAsync<ExamenSubModuloDto?>(
                $"{_baseUrlModExams}ExamenesSubM/{id}", Options._jsonOptions
                );
        }

        public async Task<List<ExamenIntentoDto>> ObtenerIntentosPorUsuarioAsync(string userId)
        {
            return await _http.GetFromJsonAsync<List<ExamenIntentoDto>>(
                $"{_baseUrlModExams}ExamIntento/usuario/{userId}", Options._jsonOptions
                ) ?? [];
        }

        public async Task<ExamenIntentoDto?> ObtenerUltimoIntentoPorUsuarioYExamenAsync(string userId, int examenId)
        {
            return await _http.GetFromJsonAsync<ExamenIntentoDto?>(
                $"{_baseUrlModExams}ExamIntento/usuario/{userId}/examen/{examenId}", Options._jsonOptions
                );
        }

        public async Task<ExamenIntentoDto?> RegistrarIntentoExamenAsync(ExamenIntentoDto intentoDto)
        {
            var response = await _http.PostAsJsonAsync(
                $"{_baseUrlModExams}ExamIntento/registrar",
                intentoDto,
                Options._jsonOptions
                );
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ExamenIntentoDto>(Options._jsonOptions);
            }
            else
            {
                // Manejar el error según sea necesario
                return null;
            }
        }

        public async Task<List<ExamenModuloDto>> ObtenerExamenesModAsync()
        {
            return await _http.GetFromJsonAsync<List<ExamenModuloDto>>(
                $"{_baseUrlModExams}ExamenesMod/todos", Options._jsonOptions
                ) ?? [];
        }
    }
}
