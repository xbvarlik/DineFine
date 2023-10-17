using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DineFine.Cache;

public static class Bootstrapper
{
    public static void AddInMemoryCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.Configure<InMemoryCacheOptions>(configuration.GetSection(nameof(InMemoryCacheOptions)));
        services.TryAddSingleton<ICacheManager, CacheManager>();
    }
}