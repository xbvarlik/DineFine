using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel")]
public class RestaurantStockInfoController : BaseController<int, RestaurantStockInfo, RestaurantStockInfoViewModel, 
    RestaurantStockInfoCreateModel, RestaurantStockInfoUpdateModel, RestaurantStockInfoQueryFilterModel, MssqlContext>
{
    public RestaurantStockInfoController(RestaurantStockInfoService service) : base(service)
    {
    }
}