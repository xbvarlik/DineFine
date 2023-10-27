using System.Linq.Expressions;
using System.Security.Claims;
using DineFine.DataObjects.Entities;
using DineFine.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DineFine.Accessor.DataAccessors;

public static class DbContextExtensions
{
    
    
    public static void OnBeforeSaveChanges(string userId, IEnumerable<EntityEntry> entityEntries, int? tenantId = null)
    {
        if (tenantId != null)
        {
            entityEntries.ToList().ForEach(entityEntry =>
            {
                OnBeforeCreateEntities(entityEntry, int.Parse(userId), tenantId.Value);
                OnBeforeModifyEntities(entityEntry, int.Parse(userId), tenantId.Value);
                OnBeforeDeleteEntities(entityEntry, int.Parse(userId), tenantId.Value);
            });
        }
        entityEntries.ToList().ForEach(entityEntry =>
        {
            OnBeforeCreateEntities(entityEntry, int.Parse(userId));
            OnBeforeModifyEntities(entityEntry, int.Parse(userId));
            OnBeforeDeleteEntities(entityEntry, int.Parse(userId));
        });
    }

    private static void OnBeforeCreateEntities(EntityEntry entityEntry, int userId, int? tenantId = null)
    {
        if (entityEntry is not { Entity: BaseEntity b, State: EntityState.Added }) return;
        b.CreatedAt = DateTime.Now;
        b.UpdatedAt = DateTime.Now;
        b.CreatedBy = userId;
        b.UpdatedBy = userId;

        if (tenantId != null)
            OnBeforeCreateTenantEntity(entityEntry, tenantId.Value);
    }

    private static void OnBeforeModifyEntities(EntityEntry entityEntry, int userId, int? tenantId = null)
    {
        if (entityEntry is not { Entity: BaseEntity b, State: EntityState.Modified }) return;
        b.UpdatedAt = DateTime.Now;
        b.UpdatedBy = userId;

        if (tenantId != null)
            OnBeforeUpdateDeleteTenantEntity(entityEntry, tenantId.Value);
    }
    
    private static void OnBeforeDeleteEntities(EntityEntry entityEntry, int userId, int? tenantId = null)
    {
        if (entityEntry is not { State: EntityState.Deleted }) return;

        if (entityEntry.Entity.GetType().GetProperty("IsDeleted") != null)
        {
            entityEntry.State = EntityState.Modified;
            entityEntry.Property("IsDeleted").CurrentValue = true;
        }
        
        if (entityEntry.Entity.GetType().GetProperty("DeletedBy") != null)
            entityEntry.Property("DeletedBy").CurrentValue = userId;
            
        if (entityEntry.Entity.GetType().GetProperty("DeletedAt") != null)
            entityEntry.Property("DeletedAt").CurrentValue = DateTime.UtcNow;
        
        if (tenantId != null)
            OnBeforeUpdateDeleteTenantEntity(entityEntry, tenantId.Value);
    }

    public static void OnBeforeReadEntities(ModelBuilder builder, int? tenantId = null)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if(IsIdentityEntity(entityType)) continue;
            SoftDeleteFilter(builder, entityType);

            TenantIdFilter(builder, entityType, tenantId);
        }
    }
    
    private static void SoftDeleteFilter(ModelBuilder builder, IMutableEntityType entityType)
    {
        var isDeletedProperty = entityType.FindProperty("IsDeleted");
        
        if (isDeletedProperty == null || isDeletedProperty.ClrType != typeof(bool)) return;
            
        var parameter = Expression.Parameter(entityType.ClrType);
        var isDeletedProp = Expression.PropertyOrField(parameter, "IsDeleted");
            
        var filter = Expression.Lambda(Expression.Not(isDeletedProp), parameter);
            
        builder.Entity(entityType.ClrType).HasQueryFilter(filter);
    }

    private static void OnBeforeCreateTenantEntity(EntityEntry entityType, int tenantId)
    {
        if(entityType.Entity.GetType().GetProperty("RestaurantId") == null) return;

        entityType.Property("RestaurantId").CurrentValue = tenantId;
    }

    private static void OnBeforeUpdateDeleteTenantEntity(EntityEntry entityType, int tenantId)
    {
        if(entityType.Entity.GetType().GetProperty("RestaurantId") == null) return;

        if(entityType.Property("RestaurantId").CurrentValue is int restaurantId && restaurantId != tenantId)
            throw new DineFineOperationalException("Tenant Id must match your restaurant Id.");
    }

    private static void TenantIdFilter(ModelBuilder builder, IMutableEntityType entityType, int? tenantId)
    {
        var tenantIdProperty = entityType.FindProperty("RestaurantId");
        if (tenantId == null || tenantIdProperty == null || tenantIdProperty.ClrType != typeof(int)) return;
        
        var tenantIdEntityNames = new List<string> { "RestaurantCategory", "RestaurantStockInfo" };
        if(!tenantIdEntityNames.Contains(entityType.Name)) return;
        
        var parameterExpression = Expression.Parameter(entityType.ClrType);
        var tenantIdProp = Expression.PropertyOrField(parameterExpression, "RestaurantId");

        var constantExpression = Expression.Constant(tenantId.Value);
        var equalExpression = Expression.Equal(tenantIdProp, constantExpression);
        
        var filter = Expression.Lambda(equalExpression, parameterExpression);
        
        builder.Entity(entityType.ClrType).HasQueryFilter(filter);
    }

    private static bool IsIdentityEntity(IReadOnlyTypeBase entityType)
    {
        var identityEntityNames = new List<string>{"User", "Role", "UserRole", "UserClaim", "UserLogin", "UserToken"};
        return identityEntityNames.Contains(entityType.Name);
    }
    
    public static IQueryable<TEntity> WithTenantId<TEntity>(this IQueryable<TEntity> query, int tenantId)
        where TEntity : class
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var property = Expression.Property(parameter, "RestaurantId");
        var constant = Expression.Constant(tenantId);
        var equals = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

        return query.Where(lambda);
    }
}