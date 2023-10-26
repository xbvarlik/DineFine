using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DineFine.Cache.RedisCache;

public static class Bootstrapper
{
    public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisSettings>(
            configuration.GetSection("RedisSettings"));
        
        services.TryAddSingleton<IRedisCacheManager, RedisCacheManager>();
    }
}