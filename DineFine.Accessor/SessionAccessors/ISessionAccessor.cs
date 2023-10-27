namespace DineFine.Accessor.SessionAccessors;

public interface ISessionAccessor
{
    string AccessUserId();
    int? AccessTenantId();
    Task<string> AccessUserIdAsync();
    Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func, string key) where T : class?;
    string? GetAccessToken();
    void AddObjectToCache<T>(string key, T value) where T : class;
    void UpdateObjectInCache<T>(string key, T value) where T : class;
    void RemoveObjectFromCache(string key);
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;
    string? GetTableSessionAccessKey();

}