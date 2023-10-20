using DineFine.Accessor;
using DineFine.Cache;

namespace DineFine.API;

public static class Bootstrapper
{
    public static void AddBootstrapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDatabases(configuration);
        services.AddSessionAccessor();
        services.AddInMemoryCache(configuration);
        services.AddHttpContextAccessor();
    }
    
    private static void AddApplicationDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSqlContext(configuration);
        services.AddCosmosContext(configuration);
    }
}