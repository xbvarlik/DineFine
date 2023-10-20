using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class RestaurantCategoryService : BaseService<int, RestaurantCategory, RestaurantCategoryViewModel, 
    RestaurantCategoryCreateModel, RestaurantCategoryUpdateModel, RestaurantCategoryQueryFilterModel, MssqlContext>
{
    public RestaurantCategoryService(MssqlContext context) : base(context)
    {
    }

    protected override IQueryable<RestaurantCategory> GetEntityDbSetAsQueryable()
    {
        return Context.RestaurantCategories
            .Include(x => x.Category)
            .Include(x => x.MenuItems)!
            .ThenInclude(x => x.MenuItemIngredients)!
            .ThenInclude(x => x.Ingredient);
    }

    protected override Task<IEnumerable<RestaurantCategoryViewModel>> OnAfterGetAllAsync(IEnumerable<RestaurantCategory> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToRestaurantCategoryViewModelList());
    }

    protected override RestaurantCategoryViewModel? OnAfterGet(RestaurantCategory? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<RestaurantCategory> OnBeforeCreateAsync(RestaurantCategoryCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<RestaurantCategoryViewModel> OnAfterCreateAsync(RestaurantCategory entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<RestaurantCategory> OnBeforeUpdateAsync(RestaurantCategory entity, RestaurantCategoryUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<RestaurantCategoryViewModel> OnAfterUpdateAsync(RestaurantCategory entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}