using DineFine.Cache;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Http;

namespace DineFine.Accessor.SessionAccessors;

public class SessionAccessor : ISessionAccessor
{
    private readonly ICacheManager _cacheManager;
    private readonly string? _accessToken;
    
    public SessionAccessor(IHttpContextAccessor httpContextAccessor, ICacheManager cacheManager)
    {
        _cacheManager = cacheManager;
        _accessToken = httpContextAccessor.HttpContext?.Request.Headers["Authorization"];
    }

    public string AccessUserId()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        
        if (_accessToken != null)
            userSessionInfo = _cacheManager.GetAsync<UserSession<TokenModel>>(_accessToken).Result;
        
        return userSessionInfo?.UserId ?? "0";
    }

    public int? AccessTenantId()
    {
        UserSession<TokenModel>? userSessionInfo = null;

        if (_accessToken != null)
            userSessionInfo = _cacheManager.GetAsync<UserSession<TokenModel>>(_accessToken).Result;

        if(userSessionInfo != null && userSessionInfo.TenantId != 0)
            return userSessionInfo.TenantId;
        
        var tableSessionInfo = _cacheManager.GetAsync<TableSession>(userSessionInfo!.UserId).Result;
        
        if(tableSessionInfo?.RestaurantId != null)
            return int.Parse(tableSessionInfo?.RestaurantId);

        return null;
    }

    public async Task<string> AccessUserIdAsync()
    {
        UserSession<TokenModel>? userSessionInfo = null;
        
        if(_accessToken != null)
            userSessionInfo = await _cacheManager.GetAsync<UserSession<TokenModel>>(_accessToken);
        
        return userSessionInfo?.UserId ?? "0";
    }

    public async Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func) where T : class?
        => await _cacheManager.GetOrAddAsync(GetAccessToken()!, func);
    
    public string? GetAccessToken() 
    {
        if(_accessToken != null && _accessToken.Contains("Bearer "))
            return _accessToken.Replace("Bearer ", "");
        
        return _accessToken;
    }
}

