using System.Linq.Expressions;
using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.DataObjects.Models;
using DineFine.Exception;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public abstract class BaseCosmosService<TEntity, TViewModel, TCreateModel, TUpdateModel, TQueryFilterModel>
    where TEntity: class
    where TViewModel: BaseViewModel
    where TCreateModel: BaseCreateModel
    where TUpdateModel: BaseUpdateModel
    where TQueryFilterModel: BaseQueryFilterModel?

{
    protected readonly CosmosContext Context;
    protected readonly string PartitionKey;

    protected BaseCosmosService(string partitionKey, CosmosContext context)
    {
        PartitionKey = partitionKey;
        Context = context;
    }

    protected virtual IQueryable<TEntity> GetEntityDbSetWithPartitionKey() => Context.Set<TEntity>().WithPartitionKey(PartitionKey);

    protected virtual DbSet<TEntity> GetEntityDbSet() => Context.Set<TEntity>();

    public virtual async Task<IEnumerable<TViewModel>> GetAllAsync(TQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = GetEntityDbSetWithPartitionKey();
            var filteredEntities = await QuerySpecification(queryFilter, entities).ToListAsync(cancellationToken);
            
            return await OnAfterGetAllAsync(filteredEntities, cancellationToken);
        }
        catch (System.Exception e)
        {
            throw DynamicExceptions.DatabaseException();
        }
    }
    
    public virtual async Task<TViewModel> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetEntityDbSetWithPartitionKey().FirstOrDefaultAsync(GenerateLambdaExpressionForId(id), cancellationToken);
            var result = OnAfterGet(entity, cancellationToken);
            
            if (result == null)
                throw DynamicExceptions.NotFoundException();
            
            return result;
        }
        catch (System.Exception e)
        {
            throw DynamicExceptions.DatabaseException();
        }
    }
    
    public virtual async Task<TViewModel> CreateAsync(TCreateModel createModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await OnBeforeCreateAsync(createModel, cancellationToken);
            await GetEntityDbSet().AddAsync(entity, cancellationToken);
            await Context.SaveChangesAsync(cancellationToken);
            return await OnAfterCreateAsync(entity, cancellationToken);
        }
        catch (System.Exception e)
        {
            throw DynamicExceptions.DatabaseException();
        }
    }
    
    public virtual async Task<TViewModel> UpdateAsync(string id, TUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetEntityDbSet().FirstOrDefaultAsync(GenerateLambdaExpressionForId(id), cancellationToken);
            
            if(entity == null)
                throw DynamicExceptions.NotFoundException();
            
            entity = await OnBeforeUpdateAsync(entity, updateModel, cancellationToken);
            GetEntityDbSet().Update(entity);
            await Context.SaveChangesAsync(cancellationToken);
            return await OnAfterUpdateAsync(entity, cancellationToken);
        }
        catch (System.Exception e)
        {
            throw DynamicExceptions.DatabaseException();
        }
    }
    
    public virtual async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await GetEntityDbSet().FirstOrDefaultAsync(GenerateLambdaExpressionForId(id), cancellationToken);
            
            if(entity == null)
                throw DynamicExceptions.NotFoundException();
            
            GetEntityDbSet().Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }
        catch (System.Exception e)
        {
            throw DynamicExceptions.DatabaseException();
        }
    }

    protected abstract Task<IEnumerable<TViewModel>> OnAfterGetAllAsync(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default);
    
    protected abstract TViewModel? OnAfterGet(TEntity? entity, CancellationToken cancellationToken = default);
    
    protected abstract Task<TEntity> OnBeforeCreateAsync(TCreateModel createModel, CancellationToken cancellationToken = default);
    
    protected abstract Task<TViewModel> OnAfterCreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    protected abstract Task<TEntity> OnBeforeUpdateAsync(TEntity entity, TUpdateModel updateModel, CancellationToken cancellationToken = default);
    
    protected abstract Task<TViewModel> OnAfterUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    private static IQueryable<TEntity> QuerySpecification(TQueryFilterModel? query, IQueryable<TEntity> queryableData)
    {
        if (query == null)
            return queryableData;
        
        var properties = typeof(TQueryFilterModel).GetProperties().ToList();
        foreach (var property in properties)
        {
            var value = property.GetValue(query);
                
            if (value != null)
            {
                queryableData = ApplyPropertyFilter(queryableData, property.Name, value);
            }
        }
            
        return queryableData;
    }
    
    private static IQueryable<TEntity> ApplyPropertyFilter(IQueryable<TEntity> queryable, string propertyName, object? value)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "entity");
        var property = Expression.Property(parameter, propertyName);
        
        if (property.Type.IsGenericType && property.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            var underlyingType = Nullable.GetUnderlyingType(property.Type);
            var convertedValue = value != null ? Convert.ChangeType(value, underlyingType!) : null;

            var hasValueProperty = Expression.Property(property, "HasValue");
            var getValueProperty = Expression.Property(property, "Value");
            var equals = Expression.Equal(getValueProperty, Expression.Constant(convertedValue));
            
            var condition = Expression.AndAlso(hasValueProperty, equals);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(condition, parameter);

            return queryable.Where(lambda);
        }
        else
        {
            var equals = Expression.Equal(property, Expression.Constant(value));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);

            return queryable.Where(lambda);
        }
    }
    
    private static Expression<Func<TEntity, bool>> GenerateLambdaExpressionForId(string id)
    {
        var mappingEntityParameterExpression = Expression.Parameter(typeof(TEntity));
        var memberExpression = Expression.PropertyOrField(mappingEntityParameterExpression, "Id");
        var constantExpression = Expression.Constant(id);

        var nonDeleteExpression = Expression.PropertyOrField(mappingEntityParameterExpression, "IsDeleted");
        var nonDeleteConstantExpression = Expression.Constant(false);

        var deleteExpression = Expression.Equal(nonDeleteExpression, nonDeleteConstantExpression);
        var binaryExpression = Expression.Equal(memberExpression, constantExpression);

        var combinedExpression = Expression.AndAlso(binaryExpression, deleteExpression);

        return Expression.Lambda<Func<TEntity, bool>>(combinedExpression, mappingEntityParameterExpression);
    }
}