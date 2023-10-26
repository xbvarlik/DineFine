namespace DineFine.Cache.RedisCache;

public interface IRedisCacheManager
{
    TCacheObject CreateCacheObject<TCacheObject, TEntity>(TEntity entity, string key, Func<TEntity, string, TCacheObject> mapper)
        where TCacheObject : class
        where TEntity : class;
    
    TCacheObject? GetCacheObject<TCacheObject>(string key) where TCacheObject : class;
    
    void RemoveCacheObject(string key);
    
    void RefreshCacheObject<TCacheObject>(string key);
    
    TCacheObject? UpdateAndRefreshCacheObject<TCacheObject, TUpdateModel>(string key, TUpdateModel updateModel, Func<TUpdateModel, TCacheObject, TCacheObject> mapper) 
        where TCacheObject : class where TUpdateModel : class;
}