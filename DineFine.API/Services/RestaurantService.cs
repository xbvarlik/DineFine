using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class RestaurantService : BaseService<int, Restaurant, RestaurantViewModel, RestaurantCreateModel, 
    RestaurantUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public RestaurantService(MssqlContext context) : base(context)
    {
    }

    protected override IQueryable<Restaurant> GetEntityDbSetAsQueryable()
    {
        return Context.Restaurants
            .Include(x => x.MenuItems)
            .Include(x => x.RestaurantCategories)
            .Include(x => x.TablesOfRestaurant)
            .AsQueryable();
    }

    protected override Task<IEnumerable<RestaurantViewModel>> OnAfterGetAllAsync(IEnumerable<Restaurant> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToRestaurantViewModelList());
    }

    protected override RestaurantViewModel? OnAfterGet(Restaurant? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<Restaurant> OnBeforeCreateAsync(RestaurantCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<RestaurantViewModel> OnAfterCreateAsync(Restaurant entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<Restaurant> OnBeforeUpdateAsync(Restaurant entity, RestaurantUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<RestaurantViewModel> OnAfterUpdateAsync(Restaurant entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}