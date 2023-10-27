using System.Security.Claims;
using DineFine.Accessor.SessionAccessors;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.Accessor.DataAccessors.Cosmos;

public class CosmosContext : DbContext
{
    public virtual DbSet<Order> Orders { get; set; } = null!;
    public virtual DbSet<TableSession> TableSessions { get; set; } = null!;
    public virtual DbSet<UserSession<TokenModel>> UserSessions { get; set; } = null!;

    private readonly ISessionAccessor? _sessionAccessor;
    
    public CosmosContext()
    {

    }
    
    public CosmosContext(DbContextOptions options, ISessionAccessor sessionAccessor) : base(options)
    {
        _sessionAccessor = sessionAccessor; 
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().ToContainer(nameof(Orders)).HasPartitionKey(x => x.RestaurantId);
        modelBuilder.Entity<TableSession>().ToContainer(nameof(TableSessions)).HasPartitionKey(x => x.RestaurantId);
        modelBuilder.Entity<UserSession<TokenModel>>().ToContainer(nameof(UserSessions)).HasPartitionKey(x => x.UserId);
        base.OnModelCreating(modelBuilder);
    }

    public int SaveChanges()
    {
        var userId = _sessionAccessor.AccessUserId();
        DbContextExtensions.OnBeforeSaveChanges(userId, ChangeTracker.Entries());
        return base.SaveChanges();
    }
    
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var userId = _sessionAccessor.AccessUserId();
        DbContextExtensions.OnBeforeSaveChanges(userId, ChangeTracker.Entries());
        return base.SaveChangesAsync(cancellationToken);
    }
}