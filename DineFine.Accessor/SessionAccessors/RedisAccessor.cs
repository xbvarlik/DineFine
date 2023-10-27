using DineFine.Accessor.HttpAccessors;
using DineFine.Cache.RedisCache;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;
using Microsoft.Extensions.Options;

namespace DineFine.Accessor.SessionAccessors;

public class RedisAccessor
{
    private readonly IRedisCacheManager _cacheManager;
    private readonly string? _accessToken;
    private readonly string? _tableSessionAccessKey = null;
    private readonly string _userPrefix;
    private readonly string _tablePrefix;

    public RedisAccessor(IRedisCacheManager cacheManager, HttpAccessor httpAccessor, IOptions<RedisSettings> redisSettings)
    {
        _cacheManager = cacheManager;
        _accessToken = httpAccessor.GetHeader("Authorization");
        _tableSessionAccessKey = httpAccessor.GetHeader("TableSessionAccessKey");
        _userPrefix = redisSettings.Value.UserSessionPrefix;
        _tablePrefix = redisSettings.Value.TableSessionPrefix;
    }
    
    public string AccessUserId()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        
        if (_accessToken != null)
            userSessionInfo = _cacheManager.GetCacheObject<UserSession<TokenModel>>(_userPrefix,GetAccessToken()!);
        
        return userSessionInfo?.UserId ?? "0";
    }
    
    public int? AccessTenantId()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        TableSession? tableSessionInfo = null;

        if (_accessToken != null)
            userSessionInfo = _cacheManager.GetCacheObject<UserSession<TokenModel>>(_userPrefix,GetAccessToken()!);

        if(userSessionInfo != null && userSessionInfo.TenantId != 0)
            return userSessionInfo.TenantId;
        
        if(_tableSessionAccessKey != null)
            tableSessionInfo = _cacheManager.GetCacheObject<TableSession>(_tablePrefix,GetTableSessionAccessKey()!);
        
        if(tableSessionInfo != null)
            return int.Parse(tableSessionInfo.RestaurantId);

        return null;
    }
    
    public string? GetAccessToken() 
    {
        if(_accessToken != null && _accessToken.Contains("Bearer "))
            return _accessToken.Replace("Bearer ", "");
        
        return _accessToken;
    }
    
    public string? GetTableSessionAccessKey() => _tableSessionAccessKey;
}