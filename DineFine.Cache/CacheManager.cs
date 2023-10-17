using DineFine.Exception;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace DineFine.Cache;

public class CacheManager : ICacheManager
{
    private readonly IMemoryCache _memoryCache;
    private readonly int _cacheExpireTimeInSeconds;

    public CacheManager(IMemoryCache memoryCache, IOptions<InMemoryCacheOptions> cacheOptions)
    {
        _memoryCache = memoryCache;
        _cacheExpireTimeInSeconds = cacheOptions.Value.ExpireTimeInSeconds;
    }

    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        => _memoryCache.TryGetValue(key, out var value) ? Task.FromResult((T?)value) : Task.FromResult<T?>(null);

    public void Update<T>(string key, T value) where T : class
    {
        _memoryCache.Remove(key);
        Set(key, value);
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void Set<T>(string key, T value) where T : class?
    {
        if (value is null) throw DynamicExceptions.OperationalException(message: "Cache value cannot be null");
        
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(_cacheExpireTimeInSeconds));
        
        _memoryCache.Set(key, value, cacheEntryOptions);

        Console.WriteLine($"Cache key: {key} is set to value {value}");
        
    }

    public async Task<T?> GetOrAddAsync<T>(string key, Func<string, Task<T?>> func, CancellationToken cancellationToken = default) where T : class?
    {
        if (_memoryCache.TryGetValue(key, out var value))
        {
            Console.WriteLine($"Cache key: {key} is found");
            return (T?)value;
        }
            

        var obj = await func(key);
        if (obj is null)
            return null;

        Set(key, obj);
        return obj;
    }

    public bool FlushCache()
    {
        ((MemoryCache)_memoryCache).Compact(1.0);
        return true;
    }
    

}