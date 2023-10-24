using DineFine.DataObjects.Entities;

namespace DineFine.DataObjects.Models;

public class TableSessionCreateModel : BaseCreateModel
{
    public string RestaurantId { get; set; } = null!;
    public RestaurantCreateModel Restaurant { get; set; } = null!;
    public ICollection<OrderCreateModel> Orders { get; set; } = null!;
    public TableOfRestaurantCreateModel TableOfRestaurant { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public double TotalPrice { get; set; }
    public CustomerReviewCreateModel? CustomerReview { get; set; }
}

public class TableSessionUpdateModel : BaseUpdateModel
{
    public ICollection<OrderCreateModel>? Orders { get; set; }
    public TableOfRestaurantCreateModel? TableOfRestaurant { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public double? TotalPrice { get; set; }
    public CustomerReviewUpdateModel? CustomerReview { get; set; }
}

public class TableSessionViewModel : BaseViewModel
{
    public RestaurantCosmosViewModel Restaurant { get; set; } = null!;
    public IEnumerable<OrderViewModel> Orders { get; set; } = null!;
    public TableOfRestaurantViewModel TableOfRestaurant { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public double TotalPrice { get; set; }
    public virtual CustomerReviewViewModel? CustomerReview { get; set; }
}

public class TableSessionQueryFilterModel : BaseCosmosQueryFilterModel
{
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
}