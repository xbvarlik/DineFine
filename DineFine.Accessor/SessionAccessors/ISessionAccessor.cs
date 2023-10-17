namespace DineFine.Accessor.SessionAccessors;

public interface ISessionAccessor
{
    int AccessUserId();
    int AccessTenantId();
    Task<int> AccessUserIdAsync();
    Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func) where T : class?;
    string? GetAccessToken();
}