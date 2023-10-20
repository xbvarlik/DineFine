using System.Security.Claims;
using DineFine.Accessor.SessionAccessors;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.Accessor.DataAccessors.Cosmos;

public class CosmosContext : DbContext
{
    //TODO: Implement Cosmos Base Service With Partition Keys
    
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<TableSession> TableSessions { get; set; } = null!;
    public DbSet<UserSession<TokenModel>> UserSessions { get; set; } = null!;

    private readonly ISessionAccessor _sessionAccessor;
    
    public CosmosContext(ISessionAccessor sessionAccessor)
    {
        _sessionAccessor = sessionAccessor;
    }
    
    public CosmosContext(DbContextOptions options, ISessionAccessor sessionAccessor) : base(options)
    {
        _sessionAccessor = sessionAccessor;
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToContainer(nameof(Orders)).HasPartitionKey(x => x.Restaurant.Id);
        modelBuilder.Entity<TableSession>().ToContainer(nameof(TableSessions)).HasPartitionKey(x => x.RestaurantId);
        modelBuilder.Entity<UserSession<TokenModel>>().ToContainer(nameof(UserSessions)).HasPartitionKey(x => x.UserId);
        base.OnModelCreating(modelBuilder);
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
}