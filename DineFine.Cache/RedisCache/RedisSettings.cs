using StackExchange.Redis;

namespace DineFine.Cache.RedisCache;

public class RedisSettings
{
    public string ConnectionString { get; set; } = null!;
    
    public string UserSessionPrefix { get; set; } = null!;
    
    public string TableSessionPrefix { get; set; } = null!;
}

public class RedisDbAndSessionKeyModel
{
    public IDatabase Database { get; set; } = null!;
    public string Key { get; set; } = null!;
}