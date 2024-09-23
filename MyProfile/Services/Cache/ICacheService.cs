namespace MyProfile.Services.Cache;

public interface ICacheService
{
    Task<T?> GetCachedDataAsync<T>(string key);
    Task SetCachedDataAsync<T>(string key, T data, TimeSpan? expirationTime = null);
}