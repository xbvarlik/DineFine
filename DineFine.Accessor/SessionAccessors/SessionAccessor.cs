using DineFine.Accessor.HttpAccessors;
using DineFine.Cache;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.SessionAccessors;

public class SessionAccessor : ISessionAccessor
{
    private readonly ICacheManager _cacheManager;
    private readonly string? _accessToken;
    private readonly string? _tableSessionAccessKey = null;
    
    public SessionAccessor(ICacheManager cacheManager, HttpAccessor httpAccessor)
    {
        _cacheManager = cacheManager;
        _accessToken = httpAccessor.GetHeader("Authorization");
        _tableSessionAccessKey = httpAccessor.GetHeader("TableSessionAccessKey");
    }

    public string AccessUserId()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        
        if (_accessToken != null)
            userSessionInfo = _cacheManager.GetAsync<UserSession<TokenModel>>(GetAccessToken()!).Result;
        
        return userSessionInfo?.UserId ?? "0";
    }

    public int? AccessTenantId()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        TableSession? tableSessionInfo = null;

        if (_accessToken != null)
            userSessionInfo = _cacheManager.GetAsync<UserSession<TokenModel>>(GetAccessToken()!).Result;

        if(userSessionInfo != null && userSessionInfo.TenantId != 0)
            return userSessionInfo.TenantId;
        
        if(_tableSessionAccessKey != null)
            tableSessionInfo = _cacheManager.GetAsync<TableSession>(GetTableSessionAccessKey()!).Result;
        
        if(tableSessionInfo != null)
            return int.Parse(tableSessionInfo.RestaurantId);

        return null;
    }

    public async Task<string> AccessUserIdAsync()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        
        if(_accessToken != null)
            userSessionInfo = await _cacheManager.GetAsync<UserSession<TokenModel>>(_accessToken);
        
        return userSessionInfo?.UserId ?? "0";
    }

    public async Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func, string key) where T : class?
        => await _cacheManager.GetOrAddAsync(key, func);

    public void AddObjectToCache<T>(string key, T value) where T : class
        => _cacheManager.Set(key, value);

    public void UpdateObjectInCache<T>(string key, T value) where T : class
        => _cacheManager.Update(key, value);

    public void RemoveObjectFromCache(string key) => _cacheManager.Remove(key);
    
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        => await _cacheManager.GetAsync<T>(key, cancellationToken);
    
    public string? GetAccessToken() 
    {
        if(_accessToken != null && _accessToken.Contains("Bearer "))
            return _accessToken.Replace("Bearer ", "");
        
        return _accessToken;
    }
    
    public string? GetTableSessionAccessKey() => _tableSessionAccessKey;
}

