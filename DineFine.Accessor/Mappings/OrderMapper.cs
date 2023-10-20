using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class OrderMapper
{
    public static Order ToEntity(this OrderCreateModel model)
    {
        return new Order
        {
            RestaurantId = model.RestaurantId,
            CustomerReview = model.CustomerReview?.ToViewModel() ?? null,
            Restaurant = model.Restaurant.ToEntity().ToViewModel(false),
            MenuItem = model.MenuItem.ToEntity().ToViewModel(false),
            TableOfRestaurant = model.TableOfRestaurant.ToEntity().ToViewModel(),
            OrderStatus = model.OrderStatus
        };
    } 
    
    public static void ToUpdatedEntity(this Order entity, OrderUpdateModel model)
    {
        entity.CustomerReview.ToUpdatedViewModel(model.CustomerReview);
        entity.MenuItem = model.MenuItem?.ToEntity().ToViewModel(false) ?? entity.MenuItem;
        entity.TableOfRestaurant = model.TableOfRestaurant?.ToEntity().ToViewModel() ?? entity.TableOfRestaurant;
        entity.OrderStatus = model.OrderStatus ?? entity.OrderStatus;
    }
    
    public static OrderViewModel ToViewModel(this Order entity)
    {
        return new OrderViewModel
        {
            OrderId = entity.OrderId,
            CustomerReview = entity.CustomerReview,
            RestaurantId = entity.Restaurant.Id,
            Restaurant = entity.Restaurant,
            MenuItemId = entity.MenuItem.Id,
            MenuItem = entity.MenuItem,
            TableOfRestaurantId = entity.TableOfRestaurant.Id,
            TableOfRestaurant = entity.TableOfRestaurant,
            OrderStatusId = entity.OrderStatus.Id,
            OrderStatus = entity.OrderStatus
        };
    }
    
    public static IEnumerable<OrderViewModel> ToViewModelList(this IEnumerable<Order> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}