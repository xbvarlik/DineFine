using System.Security.Claims;
using DineFine.Accessor.SessionAccessors;
using DineFine.DataObjects.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

    private readonly ISessionAccessor? _sessionAccessor;
    
    public MssqlContext()
    {
    }

    public MssqlContext(DbContextOptions<MssqlContext> options) : base(options)
    {
    }
    
    public MssqlContext(DbContextOptions<MssqlContext> options, ISessionAccessor sessionAccessor) : base(options)
    {
        _sessionAccessor = sessionAccessor;
    }

    public override int SaveChanges()
    {
        var userId = _sessionAccessor!.AccessUserId();
        var tenantId = _sessionAccessor.AccessTenantId();
        ContextEventHandlers.OnBeforeSaveChanges(userId, ChangeTracker.Entries(), tenantId);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = _sessionAccessor!.AccessUserId();
        var tenantId = _sessionAccessor.AccessTenantId();
        ContextEventHandlers.OnBeforeSaveChanges(userId, ChangeTracker.Entries(), tenantId);
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SeedInitialData();
        var tenantId = _sessionAccessor!.AccessTenantId();
        ContextEventHandlers.OnBeforeReadEntities(builder, tenantId);
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