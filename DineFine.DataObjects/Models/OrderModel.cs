namespace DineFine.DataObjects.Models;

public class OrderCreateModel : BaseCreateModel
{
    public int RestaurantId { get; set; }
    public CustomerReviewCreateModel? CustomerReview { get; set; }
    public RestaurantCreateModel Restaurant { get; set; } = null!;
    public TableOfRestaurantCreateModel TableOfRestaurant { get; set; } = null!;
    public OrderStatusViewModel OrderStatus { get; set; } = null!;
    public MenuItemCreateModel MenuItem { get; set; } = null!;
}

public class OrderUpdateModel : BaseUpdateModel
{
    public CustomerReviewUpdateModel? CustomerReview { get; set; }
    public TableOfRestaurantCreateModel? TableOfRestaurant { get; set; }
    public OrderStatusViewModel? OrderStatus { get; set; }
    public MenuItemCreateModel? MenuItem { get; set; }
}

public class OrderViewModel : BaseViewModel
{
    public string OrderId { get; set; } = null!;
    public virtual CustomerReviewViewModel? CustomerReview { get; set; }
    public int RestaurantId { get; set; }
    public virtual RestaurantViewModel? Restaurant { get; set; }
    public int TableOfRestaurantId { get; set; }
    public virtual TableOfRestaurantViewModel? TableOfRestaurant { get; set; }
    public int OrderStatusId { get; set; }
    public virtual OrderStatusViewModel? OrderStatus { get; set; }
    public int MenuItemId { get; set; }
    public virtual MenuItemViewModel? MenuItem { get; set; }
}