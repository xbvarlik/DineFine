using DineFine.Exception;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DineFine.Cache.RedisCache;

public class RedisCacheManager : IRedisCacheManager
{
    private readonly ConnectionMultiplexer _redis;
    private readonly RedisSettings _redisSettings;
    private const int ExpireTime = 15;

    public RedisCacheManager(IOptions<RedisSettings> redisSettings)
    {
        _redis = ConnectionMultiplexer.Connect(redisSettings.Value.ConnectionString);
        _redisSettings = redisSettings.Value;
    }
    
    private RedisDbAndSessionKeyModel GetRedisDbAndKey(string key)
    {
        return new RedisDbAndSessionKeyModel
        {
            Database = _redis.GetDatabase(), 
            Key = $"{_redisSettings.RedisPrefix}:{key}:session"
        };
    }

    public TCacheObject CreateCacheObject<TCacheObject, TEntity>(TEntity entity, string key, Func<TEntity, string, TCacheObject> mapper) 
        where TCacheObject : class
        where TEntity : class
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key);

        var cacheObject = mapper(entity, getRedisDbAndKey.Key);

        var jsonData = JsonConvert.SerializeObject(cacheObject);

        getRedisDbAndKey.Database.StringSet(getRedisDbAndKey.Key, jsonData, TimeSpan.FromMinutes(ExpireTime));

        return cacheObject;
    }

    public TCacheObject? GetCacheObject<TCacheObject>(string key) where TCacheObject : class
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key);

        var jsonData = getRedisDbAndKey.Database.StringGet(getRedisDbAndKey.Key);

        if (jsonData.IsNullOrEmpty) 
            return null;
        
        var session = JsonConvert.DeserializeObject<TCacheObject>(jsonData);
        
        return session;
    }

    public void RemoveCacheObject(string key)
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key);
        
        getRedisDbAndKey.Database.KeyDelete(getRedisDbAndKey.Key);
    }

    public void RefreshCacheObject<TCacheObject>(string key)
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key);
        
        var jsonData = getRedisDbAndKey.Database.StringGet(getRedisDbAndKey.Key);
        
        if (jsonData.IsNullOrEmpty) 
            return;
        
        var cacheObject = JsonConvert.DeserializeObject<TCacheObject>(jsonData);
        
        jsonData = JsonConvert.SerializeObject(cacheObject);
        
        getRedisDbAndKey.Database.StringSet(getRedisDbAndKey.Key, jsonData, TimeSpan.FromMinutes(ExpireTime));
    }

    public TCacheObject? UpdateAndRefreshCacheObject<TCacheObject, TUpdateModel>(string key, TUpdateModel updateModel, Func<TUpdateModel, TCacheObject, TCacheObject> mapper) 
        where TCacheObject : class where TUpdateModel : class
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key);
        
        var jsonData = getRedisDbAndKey.Database.StringGet(getRedisDbAndKey.Key);
        
        if (jsonData.IsNullOrEmpty) 
            return null;
        
        var cacheObject = JsonConvert.DeserializeObject<TCacheObject>(jsonData);
        
        cacheObject = mapper(updateModel, cacheObject!);
        
        jsonData = JsonConvert.SerializeObject(cacheObject);
        
        getRedisDbAndKey.Database.StringSet(getRedisDbAndKey.Key, jsonData, TimeSpan.FromMinutes(ExpireTime));

        return cacheObject;
    }
}