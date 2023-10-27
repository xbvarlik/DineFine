namespace DineFine.Cache.RedisCache;

public interface IRedisCacheManager
{
    TCacheObject CreateCacheObject<TCacheObject, TEntity>(TEntity entity, string key, string prefix, Func<TEntity, string, TCacheObject> mapper)
        where TCacheObject : class
        where TEntity : class;
    
    TCacheObject? GetCacheObject<TCacheObject>(string key, string prefix) where TCacheObject : class;
    
    void RemoveCacheObject(string key, string prefix);
    
    void RefreshCacheObject<TCacheObject>(string key, string prefix);
    
    TCacheObject? UpdateAndRefreshCacheObject<TCacheObject, TUpdateModel>(string key, string prefix, TUpdateModel updateModel, Func<TUpdateModel, TCacheObject, TCacheObject> mapper) 
        where TCacheObject : class where TUpdateModel : class;
}