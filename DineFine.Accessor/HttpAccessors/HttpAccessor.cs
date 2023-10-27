using Microsoft.AspNetCore.Http;

namespace DineFine.Accessor.HttpAccessors;

public class HttpAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetHeader(string key, string? value)
    {
        if(_httpContextAccessor.HttpContext != null)
            _httpContextAccessor.HttpContext.Response.Headers[key] = value;
    }
    
    public string? GetHeader(string key)
    {
        if(_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.Request.Headers.TryGetValue(key, out var header))
            return header;
        return null;
    }

    public void RemoveHeader(string key)
    {
        _httpContextAccessor.HttpContext?.Response.Headers.Remove(key);
    }
}