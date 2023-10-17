using System.Linq.Expressions;
using DineFine.Accessor.SessionAccessors;
using DineFine.DataObjects.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DineFine.Accessor.DataAccessors.Mssql;

public class MssqlContext : IdentityDbContext<User, Role, int>
{
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
    
    private readonly ISessionAccessor _sessionAccessor;
    
    public MssqlContext(ISessionAccessor sessionAccessor)
    {
        _sessionAccessor = sessionAccessor;
    }

    public MssqlContext(DbContextOptions options, ISessionAccessor sessionAccessor) : base(options)
    {
        _sessionAccessor = sessionAccessor;
    }

    public override int SaveChanges()
    {
        ContextEventHandlers.OnBeforeSaveChanges(ChangeTracker.Entries(), _sessionAccessor);
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ContextEventHandlers.OnBeforeSaveChanges(ChangeTracker.Entries(), _sessionAccessor);
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.SeedInitialData();
        ContextEventHandlers.OnBeforeReadEntities(builder, _sessionAccessor);
        base.OnModelCreating(builder);
    }
}