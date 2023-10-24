using System.Linq.Expressions;
using System.Security.Claims;
using DineFine.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DineFine.Accessor.DataAccessors;

public static class ContextEventHandlers
{
    public static void OnBeforeSaveChanges(string userId, IEnumerable<EntityEntry> entityEntries)
    {
        entityEntries.ToList().ForEach(entityEntry =>
        {
            OnBeforeCreateEntities(entityEntry, int.Parse(userId));
            OnBeforeModifyEntities(entityEntry, int.Parse(userId));
            OnBeforeDeleteEntities(entityEntry, int.Parse(userId));
        });
    }

    private static void OnBeforeCreateEntities(EntityEntry entityEntry, int userId)
    {
        if (entityEntry is not { Entity: BaseEntity b, State: EntityState.Added }) return;
        b.CreatedAt = DateTime.Now;
        b.UpdatedAt = DateTime.Now;
        b.CreatedBy = userId;
        b.UpdatedBy = userId;
    }

    private static void OnBeforeModifyEntities(EntityEntry entityEntry, int userId)
    {
        if (entityEntry is not { Entity: BaseEntity b, State: EntityState.Modified }) return;
        b.UpdatedAt = DateTime.Now;
        b.UpdatedBy = userId;
    }
    
    private static void OnBeforeDeleteEntities(EntityEntry entityEntry, int userId)
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
    }

    public static void OnBeforeReadEntities(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if(IsIdentityEntity(entityType)) continue;
            SoftDeleteFilter(builder, entityType);
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

    private static bool IsIdentityEntity(IReadOnlyTypeBase entityType)
    {
        var identityEntityNames = new List<string>{"User", "Role", "UserRole", "UserClaim", "UserLogin", "UserToken"};
        return identityEntityNames.Contains(entityType.Name);
    }
}