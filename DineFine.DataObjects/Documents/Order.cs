using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class Order
{
    public string OrderId { get; set; } = null!;
    public UserViewModel Customer { get; set; } = null!;
    public RestaurantViewModel Restaurant { get; set; } = null!;
    public MenuItemViewModel MenuItem { get; set; } = null!;
    public TableOfRestaurantViewModel TableOfRestaurant { get; set; } = null!;
    public OrderStatusViewModel OrderStatus { get; set; } = null!;
}