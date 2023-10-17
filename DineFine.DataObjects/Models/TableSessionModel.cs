namespace DineFine.DataObjects.Models;

public class TableSessionCreateModel
{
    public int RestaurantId { get; set; }
    public ICollection<OrderCreateModel> Orders { get; set; }
    public int TableOfRestaurantId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public double TotalPrice { get; set; }
    public CustomerReviewCreateModel Customer { get; set; }
}

public class TableSessionUpdateModel : BaseUpdateModel
{
    public int? RestaurantId { get; set; }
    public ICollection<OrderUpdateModel>? Orders { get; set; }
    public int? TableOfRestaurantId { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public double? TotalPrice { get; set; }
    public CustomerReviewUpdateModel? Customer { get; set; }
}

public class TableSessionViewModel : BaseViewModel
{
    public int RestaurantId { get; set; }
    public virtual RestaurantViewModel? Restaurant { get; set; }
    public ICollection<OrderViewModel>? Orders { get; set; }
    public int TableOfRestaurantId { get; set; }
    public virtual TableOfRestaurantViewModel? TableOfRestaurant { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public double TotalPrice { get; set; }
    public virtual CustomerReviewViewModel? Customer { get; set; }
}