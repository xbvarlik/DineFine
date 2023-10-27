using DineFine.Exception;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DineFine.Cache.RedisCache;

public class RedisCacheManager : IRedisCacheManager
{
    private readonly ConnectionMultiplexer _redis;
    private readonly RedisSettings _redisSettings;
    private string _prefix;
    private const int ExpireTime = 15;

    public RedisCacheManager(IOptions<RedisSettings> redisSettings)
    {
        _redis = ConnectionMultiplexer.Connect(redisSettings.Value.ConnectionString);
        _redisSettings = redisSettings.Value;
    }
    
    private RedisDbAndSessionKeyModel GetRedisDbAndKey(string key, string prefix)
    {
        _prefix = prefix;
        return new RedisDbAndSessionKeyModel
        {
            Database = _redis.GetDatabase(), 
            Key = $"{prefix}:{key}:session"
        };
    }

    public TCacheObject CreateCacheObject<TCacheObject, TEntity>(TEntity entity, string prefix, string key, Func<TEntity, string, TCacheObject> mapper) 
        where TCacheObject : class
        where TEntity : class
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key, prefix);

        var cacheObject = mapper(entity, getRedisDbAndKey.Key);

        var jsonData = JsonConvert.SerializeObject(cacheObject);

        getRedisDbAndKey.Database.StringSet(getRedisDbAndKey.Key, jsonData, TimeSpan.FromMinutes(ExpireTime));

        return cacheObject;
    }

    public TCacheObject? GetCacheObject<TCacheObject>(string prefix, string key) where TCacheObject : class
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key, prefix);

        var jsonData = getRedisDbAndKey.Database.StringGet(getRedisDbAndKey.Key);

        if (jsonData.IsNullOrEmpty) 
            return null;
        
        var session = JsonConvert.DeserializeObject<TCacheObject>(jsonData);
        
        return session;
    }

    public void RemoveCacheObject(string key, string prefix)
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key, prefix);
        
        getRedisDbAndKey.Database.KeyDelete(getRedisDbAndKey.Key);
    }

    public void RefreshCacheObject<TCacheObject>(string key, string prefix)
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key, prefix);
        
        var jsonData = getRedisDbAndKey.Database.StringGet(getRedisDbAndKey.Key);
        
        if (jsonData.IsNullOrEmpty) 
            return;
        
        var cacheObject = JsonConvert.DeserializeObject<TCacheObject>(jsonData);
        
        jsonData = JsonConvert.SerializeObject(cacheObject);
        
        getRedisDbAndKey.Database.StringSet(getRedisDbAndKey.Key, jsonData, TimeSpan.FromMinutes(ExpireTime));
    }

    public TCacheObject? UpdateAndRefreshCacheObject<TCacheObject, TUpdateModel>(string key, string prefix, TUpdateModel updateModel, Func<TUpdateModel, TCacheObject, TCacheObject> mapper) 
        where TCacheObject : class where TUpdateModel : class
    {
        var getRedisDbAndKey = GetRedisDbAndKey(key, prefix);
        
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