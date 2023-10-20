using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner")]
public class TableOfRestaurantController : BaseController<int, TableOfRestaurant, TableOfRestaurantViewModel, 
    TableOfRestaurantCreateModel, TableOfRestaurantUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    protected TableOfRestaurantController(TableOfRestaurantService service) : base(service)
    {
    }

    [SpecificAccess("Waiter, Customer")]
    public override Task<IActionResult> UpdateAsync(int id, TableOfRestaurantUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(id, updateModel, cancellationToken);
    }
}