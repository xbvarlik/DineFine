using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.SessionAccessors;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel, Waiter")]
public class RestaurantCategoryController : BaseTenantController<int, RestaurantCategory, RestaurantCategoryViewModel, 
    RestaurantCategoryCreateModel, RestaurantCategoryUpdateModel, RestaurantCategoryQueryFilterModel, MssqlContext>
{
    public RestaurantCategoryController(RestaurantCategoryService service, ISessionAccessor sessionAccessor) : base(service, sessionAccessor)
    {
    }
}