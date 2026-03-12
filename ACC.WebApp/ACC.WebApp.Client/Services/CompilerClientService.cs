using System.Net.Http.Json;
using ACC.Shared.DTOs;
using ACC.Shared.Utils;

namespace ACC.WebApp.Client.Services;

public sealed class CompilerClientService(HttpClient http)
{
    private const string CompileEndpoint = "api/compile";

    public async Task<string> CompileAsync(string code, string input, CancellationToken ct = default)
    {
        var request = new CompileRequest
        {
            Code = code,
            Input = input
        };

        var response = await http.PostAsJsonAsync(CompileEndpoint, request, Options._jsonOptions, ct);
        var payload = await response.Content.ReadAsStringAsync(ct);

        if (!response.IsSuccessStatusCode)
        {
            return $"Error: {payload}";
        }

        return payload;
    }
}
