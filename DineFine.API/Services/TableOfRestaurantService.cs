using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class TableOfRestaurantService : BaseService<int, TableOfRestaurant, TableOfRestaurantViewModel, 
    TableOfRestaurantCreateModel, TableOfRestaurantUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public TableOfRestaurantService(MssqlContext context) : base(context)
    {
    }
    
    protected override IQueryable<TableOfRestaurant> GetEntityDbSetAsQueryable()
    {
        return Context.TableOfRestaurants
            .Include(x => x.TableStatus)
            .AsQueryable();
    }

    protected override Task<IEnumerable<TableOfRestaurantViewModel>> OnAfterGetAllAsync(IEnumerable<TableOfRestaurant> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToTableOfRestaurantViewModelList());
    }

    protected override TableOfRestaurantViewModel? OnAfterGet(TableOfRestaurant? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<TableOfRestaurant> OnBeforeCreateAsync(TableOfRestaurantCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<TableOfRestaurantViewModel> OnAfterCreateAsync(TableOfRestaurant entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<TableOfRestaurant> OnBeforeUpdateAsync(TableOfRestaurant entity, TableOfRestaurantUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<TableOfRestaurantViewModel> OnAfterUpdateAsync(TableOfRestaurant entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}