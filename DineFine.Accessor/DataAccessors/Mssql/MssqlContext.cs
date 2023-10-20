using System.Security.Claims;
using DineFine.DataObjects.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DineFine.Accessor.DataAccessors.Mssql;

public class MssqlContext : IdentityDbContext<User, Role, int>
{
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

    public MssqlContext()
    {
    }

    public MssqlContext(DbContextOptions<MssqlContext> options) : base(options)
    {
    }

    public int SaveChanges(ClaimsPrincipal? user)
    {
        ContextEventHandlers.OnBeforeSaveChanges(user, ChangeTracker.Entries());
        return base.SaveChanges();
    }

    public async Task<int> SaveChangesAsync(ClaimsPrincipal? user, CancellationToken cancellationToken = new CancellationToken())
    {
        ContextEventHandlers.OnBeforeSaveChanges(user, ChangeTracker.Entries());
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SeedInitialData();
        ContextEventHandlers.OnBeforeReadEntities(builder);
        base.OnModelCreating(builder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder().Build();

        if (!optionsBuilder.IsConfigured)
            optionsBuilder
                .UseSqlServer(configuration.GetConnectionString("MssqlContext"))
                .EnableSensitiveDataLogging();
    }
}