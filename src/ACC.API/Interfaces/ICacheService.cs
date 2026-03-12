namespace ACC.API.Interfaces;

public interface ICacheService
{
    Task<(bool Found, T? Value)> TryGetAsync<T>(string key, CancellationToken cancellationToken = default);

    Task SetAsync<T>(string key, T value, TimeSpan ttl, CancellationToken cancellationToken = default);

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}

