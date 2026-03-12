using ACC.Shared.DTOs;
using ACC.Shared.Core;
using ACC.Shared.Utils;
using System.Net.Http.Json;

namespace ACC.WebApp.Client.Services;

public sealed class CharpClientService(HttpClient http)
{
    public async Task<string?> ObtenerEmbedUrlAsync(CancellationToken ct = default)
    {
        var result = await http.GetFromJsonAsync<ServiceResult<CharpEmbedConfigDto>>("Charp/embed-url", Options._jsonOptions, ct);
        if (result?.Success != true)
        {
            return null;
        }

        return result.Data?.IframeUrl;
    }
}
