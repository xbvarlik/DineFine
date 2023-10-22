using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.API.Attributes;
using DineFine.API.Services;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel, Waiter")]
public class OrderController : BaseCosmosController<Order, OrderViewModel, OrderCreateModel, OrderUpdateModel, 
    BaseCosmosQueryFilterModel>
{
    public OrderController(OrderService service) : base(service)
    {
    }
}