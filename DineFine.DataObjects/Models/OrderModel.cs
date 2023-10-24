namespace DineFine.DataObjects.Models;

public class OrderCreateModel : BaseCreateModel
{
    public string RestaurantId { get; set; } = null!;
    public OrderStatusViewModel OrderStatus { get; set; } = null!;
    public MenuItemCreateModel MenuItem { get; set; } = null!;
}

public class OrderUpdateModel : BaseUpdateModel
{
    public OrderStatusViewModel? OrderStatus { get; set; }
    public MenuItemCreateModel? MenuItem { get; set; }
}

public class OrderViewModel : BaseViewModel
{
    public string OrderId { get; set; } = null!;
    public int OrderStatusId { get; set; }
    public virtual OrderStatusViewModel? OrderStatus { get; set; }
    public int MenuItemId { get; set; }
    public virtual MenuItemCosmosViewModel? MenuItem { get; set; }
}