using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class MenuItemService : BaseService<int, MenuItem, MenuItemViewModel, MenuItemCreateModel, MenuItemUpdateModel, 
    MenuItemQueryFilterModel, MssqlContext>
{
    public MenuItemService(MssqlContext context) : base(context)
    {
    }

    protected override IQueryable<MenuItem> GetEntityDbSetAsQueryable()
    {
        return Context.MenuItems
            .Include(x => x.MenuItemIngredients)!
            .ThenInclude(x => x.Ingredient);
    }

    protected override Task<IEnumerable<MenuItemViewModel>> OnAfterGetAllAsync(IEnumerable<MenuItem> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToMenuItemViewModelList());
    }

    protected override MenuItemViewModel? OnAfterGet(MenuItem? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<MenuItem> OnBeforeCreateAsync(MenuItemCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<MenuItemViewModel> OnAfterCreateAsync(MenuItem entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override Task<MenuItem> OnBeforeUpdateAsync(MenuItem entity, MenuItemUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        entity.ToUpdatedEntity(updateModel);
        return Task.FromResult(entity);
    }

    protected override Task<MenuItemViewModel> OnAfterUpdateAsync(MenuItem entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}