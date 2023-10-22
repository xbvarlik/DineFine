using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel")]
public class MenuItemController : BaseController<int, MenuItem, MenuItemViewModel, MenuItemCreateModel, MenuItemUpdateModel, 
    MenuItemQueryFilterModel, MssqlContext>
{
    public MenuItemController(MenuItemService service) : base(service)
    {
    }

    [AllowAnonymous]
    public override Task<IActionResult> GetAllAsync(MenuItemQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        return base.GetAllAsync(queryFilter, cancellationToken);
    }

    [AllowAnonymous]
    public override Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }
}