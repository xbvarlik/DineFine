using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class OrderMapper
{
    public static Order ToEntity(this OrderCreateModel model)
    {
        return new Order
        {
            OrderId = Guid.NewGuid().ToString(),
            RestaurantId = model.RestaurantId,
            MenuItem = model.MenuItem.ToEntity().ToCosmosViewModel(),
            OrderStatus = model.OrderStatus
        };
    } 
    
    public static void ToUpdatedEntity(this Order entity, OrderUpdateModel model)
    {
        entity.MenuItem = model.MenuItem?.ToEntity().ToCosmosViewModel() ?? entity.MenuItem;
        entity.OrderStatus = model.OrderStatus ?? entity.OrderStatus;
    }
    
    public static OrderViewModel ToViewModel(this Order entity)
    {
        return new OrderViewModel
        {
            OrderId = entity.OrderId,
            MenuItemId = entity.MenuItem.Id,
            MenuItem = entity.MenuItem,
            OrderStatusId = entity.OrderStatus.Id,
            OrderStatus = entity.OrderStatus
        };
    }
    
    public static IEnumerable<OrderViewModel> ToViewModelList(this IEnumerable<Order> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}