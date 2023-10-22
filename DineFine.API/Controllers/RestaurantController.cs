using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

public class RestaurantController : BaseController<int, Restaurant, RestaurantViewModel, RestaurantCreateModel, 
    RestaurantUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    public RestaurantController(RestaurantService service) : base(service)
    {
    }

    [AllowAnonymous]
    public override Task<IActionResult> GetAllAsync(BaseQueryFilterModel? queryFilter = null, CancellationToken cancellationToken = default)
    {
        return base.GetAllAsync(queryFilter, cancellationToken);
    }

    [AllowAnonymous]
    public override Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, cancellationToken);
    }

    [SpecificAccess("RestaurantOwner")]
    public override Task<IActionResult> UpdateAsync(int id, RestaurantUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(id, updateModel, cancellationToken);
    }
}