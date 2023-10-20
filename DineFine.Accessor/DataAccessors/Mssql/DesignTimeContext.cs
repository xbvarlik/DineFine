using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DineFine.Accessor.DataAccessors.Mssql;

public class DesignTimeContext : IDesignTimeDbContextFactory<MssqlContext>
{
    public MssqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MssqlContext>();
        optionsBuilder.UseSqlServer(DataConstants.ConnectionString, b => 
            b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
        return new MssqlContext(optionsBuilder.Options);
    }
}