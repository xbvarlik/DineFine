using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.SessionAccessors;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel")]
public class RestaurantStockInfoController : BaseTenantController<int, RestaurantStockInfo, RestaurantStockInfoViewModel, 
    RestaurantStockInfoCreateModel, RestaurantStockInfoUpdateModel, RestaurantStockInfoQueryFilterModel, MssqlContext>
{
    public RestaurantStockInfoController(RestaurantStockInfoService service, ISessionAccessor sessionAccessor) : base(service, sessionAccessor)
    {
    }
}