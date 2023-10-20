using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class MenuItemIngredientService : BaseService<int, MenuItemIngredient, MenuItemIngredientViewModel, 
    MenuItemIngredientCreateModel, MenuItemIngredientUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public MenuItemIngredientService(MssqlContext context) : base(context)
    {
    }

    protected override IQueryable<MenuItemIngredient> GetEntityDbSetAsQueryable()
    {
        return Context.MenuItemIngredients
            .Include(x => x.Ingredient)
            .AsQueryable();
    }

    protected override Task<IEnumerable<MenuItemIngredientViewModel>> OnAfterGetAllAsync(IEnumerable<MenuItemIngredient> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToMenuItemIngredientViewModelList());
    }

    protected override MenuItemIngredientViewModel? OnAfterGet(MenuItemIngredient? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<MenuItemIngredient> OnBeforeCreateAsync(MenuItemIngredientCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<MenuItemIngredientViewModel> OnAfterCreateAsync(MenuItemIngredient entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<MenuItemIngredient> OnBeforeUpdateAsync(MenuItemIngredient entity, MenuItemIngredientUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<MenuItemIngredientViewModel> OnAfterUpdateAsync(MenuItemIngredient entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}