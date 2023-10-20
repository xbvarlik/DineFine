using System.Linq.Expressions;
using System.Security.Claims;
using DineFine.DataObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DineFine.Accessor.DataAccessors;

public static class ContextEventHandlers
{
    public static void OnBeforeSaveChanges(ClaimsPrincipal? user, IEnumerable<EntityEntry> entityEntries)
    {
        var userId = user == null ? 1 : int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier) ?? "1");
        
        entityEntries.ToList().ForEach(entityEntry =>
        {
            OnBeforeCreateEntities(entityEntry, userId);
            OnBeforeModifyEntities(entityEntry, userId);
            OnBeforeDeleteEntities(entityEntry, userId);
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
        if (entityEntry is not { Entity: BaseEntity b, State: EntityState.Deleted }) return;
        entityEntry.State = EntityState.Modified;
        b.DeletedAt = DateTime.Now;
        b.DeletedBy = userId;
        b.IsDeleted = true;
    }

    public static void OnBeforeReadEntities(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
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
}