using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.HttpAccessors;
using DineFine.Accessor.SessionAccessors;
using DineFine.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DineFine.Accessor;

public static class Bootstrapper
{
    public static void AddAccessors(this IServiceCollection services)
    {
        services.AddScoped<ISessionAccessor, SessionAccessor>();
        services.AddScoped<HttpAccessor>();
        services.AddScoped<RedisAccessor>();
    }
    

    public static void AddSqlContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlConnection");
        services.AddDbContext<MssqlContext>(x => x.UseSqlServer(connectionString));
    }

    public static void AddCosmosContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CosmosConnection");
        var databaseName = configuration.GetValue<string>("CosmosSettings:DatabaseName");

        if (connectionString == null || databaseName == null)
            throw new DineFineDatabaseException();
        
        services.AddDbContext<CosmosContext>(x => x.UseCosmos(connectionString, databaseName));
    }
}