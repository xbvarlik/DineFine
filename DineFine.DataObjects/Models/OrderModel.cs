namespace DineFine.DataObjects.Models;

public class OrderCreateModel : BaseCreateModel
{
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public int TableOfRestaurantId { get; set; }
    public int OrderStatusId { get; set; }
    public int MenuItemId { get; set; }
}

public class OrderUpdateModel : BaseUpdateModel
{
    public int? CustomerId { get; set; }
    public int? RestaurantId { get; set; }
    public int? TableOfRestaurantId { get; set; }
    public int? OrderStatusId { get; set; }
    public int? MenuItemId { get; set; }
}

public class OrderViewModel : BaseViewModel
{
    public virtual CustomerReviewViewModel? Customer { get; set; }
    public int RestaurantId { get; set; }
    public virtual RestaurantViewModel? Restaurant { get; set; }
    public int TableOfRestaurantId { get; set; }
    public virtual TableOfRestaurantViewModel? TableOfRestaurant { get; set; }
    public int OrderStatusId { get; set; }
    public virtual OrderStatusViewModel? OrderStatus { get; set; }
    public int MenuItemId { get; set; }
    public virtual MenuItemViewModel? MenuItem { get; set; }
}