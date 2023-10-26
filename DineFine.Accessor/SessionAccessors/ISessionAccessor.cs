namespace DineFine.Accessor.SessionAccessors;

public interface ISessionAccessor
{
    string AccessUserId();
    int? AccessTenantId();
    Task<string> AccessUserIdAsync();
    Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func) where T : class?;
    string? GetAccessToken();
}