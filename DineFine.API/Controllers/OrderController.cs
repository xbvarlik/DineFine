using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel, Waiter")]
public class OrderController : BaseController<string, Order, OrderViewModel, OrderCreateModel, OrderUpdateModel, 
    BaseQueryFilterModel, CosmosContext>
{
    protected OrderController(OrderService service) : base(service)
    {
    }
}