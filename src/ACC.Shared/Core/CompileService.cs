using ACC.Shared.DTOs;
using System.Text;
using System.Text.Json;

namespace ACC.Shared.Core
{
    public class CompileService
    {
        private readonly HttpClient _httpClient;

        public CompileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        } // loool, bueno esto es como que el modelo para el compilador en si, basicamente se encarga de enviar el codigo y el input al compilador y devolver el resultado

        public async Task<string> CompileCodeAsync(string code, string input)
        {
            try
            {
                var request = new { Code = code, Input = input };
                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7023/api/Compile", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"Error de compilación:\n{errorContent}";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error interno: {ex.Message}";
            }
        }

    }
}
