using System.Reflection;
using DineFine.DataObjects.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DineFine.Accessor.DataAccessors.Mssql;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<MssqlContext>
{
    public MssqlContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MssqlContext>();
        optionsBuilder.UseSqlServer(DataConstants.ConnectionString, b => 
            b.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name));
        return new MssqlContext(optionsBuilder.Options);
    }
}

public class DesignTimeDbContext : IdentityDbContext<User, Role, int>
{
    public DesignTimeDbContext()
    {
        
    }
    public DesignTimeDbContext(DbContextOptions<DesignTimeDbContext> options) : base(options)
    {
    }
    
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = null!;
    public DbSet<Restaurant> Restaurants { get; set; } = null!;
    public DbSet<RestaurantCategory> RestaurantCategories { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<MenuItem> MenuItems { get; set; } = null!;
    public DbSet<MenuItemIngredient> MenuItemIngredients { get; set; } = null!;
    public DbSet<Ingredient> Ingredients { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatus { get; set; } = null!;
    public DbSet<RestaurantStockInfo> RestaurantStockInfos { get; set; } = null!;
    public DbSet<TableOfRestaurant> TableOfRestaurants { get; set; } = null!;
    public DbSet<TableStatus> TableStatus { get; set; } = null!;
    public DbSet<Unit> Units { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SeedInitialData();
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder
                .UseSqlServer(DataConstants.ConnectionString, b => 
                    b.MigrationsAssembly("DineFine.Accessor.DataAccessors.Mssql"))
                .EnableSensitiveDataLogging();
    }
}