using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace MyProfile.Services.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IDistributedCache cache, ILogger<CacheService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<T?> GetCachedDataAsync<T>(string key)
    {
        try
        {
            var cachedData = await _cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedData))
            {
                _logger.LogInformation("Cache hit for key: {Key}", key);
                return JsonSerializer.Deserialize<T>(cachedData);
            }
            _logger.LogInformation("Cache miss for key: {Key}", key);
            return default;  // Если в кэше ничего нет
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting cached data");
            throw;
        }
    }

    public async Task SetCachedDataAsync<T>(string key, T data, TimeSpan? expirationTime = null)
    {
        try
        {
            var serializedData = JsonSerializer.Serialize(data);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromMinutes(5)  // Время жизни кэша по умолчанию
            };

            await _cache.SetStringAsync(key, serializedData, options);
            _logger.LogInformation("Data cached for key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting cached data");
            throw;
        }
    }
}