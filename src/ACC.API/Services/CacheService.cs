using ACC.API.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace ACC.API.Services;

public sealed class CacheService : ICacheService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly Meter CacheMeter = new("ACC.API.Cache");
    private static readonly Counter<long> CacheHits = CacheMeter.CreateCounter<long>("acc_cache_hit_total");
    private static readonly Counter<long> CacheMisses = CacheMeter.CreateCounter<long>("acc_cache_miss_total");

    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<(bool Found, T? Value)> TryGetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var payload = await _cache.GetStringAsync(key, cancellationToken);
        if (string.IsNullOrWhiteSpace(payload))
        {
            CacheMisses.Add(1);
            return (false, default);
        }

        try
        {
            var value = JsonSerializer.Deserialize<T>(payload, JsonOptions);
            CacheHits.Add(1);
            return (true, value);
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Invalid cache payload for key {CacheKey}", key);
            CacheMisses.Add(1);
            return (false, default);
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken = default)
    {
        var payload = JsonSerializer.Serialize(value, JsonOptions);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = ttl
        };

        await _cache.SetStringAsync(key, payload, options, cancellationToken);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(key, cancellationToken);
}

