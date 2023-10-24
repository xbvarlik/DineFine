namespace DineFine.DataObjects.Models;

public class CustomerReviewCreateModel : BaseCreateModel
{
    public int RestaurantId { get; set; }
    public int CustomerId { get; set; }
    public int Rating { get; set; }
    public string? Review { get; set; }
}

public class CustomerReviewUpdateModel : BaseUpdateModel
{
    public int? RestaurantId { get; set; }
    public int? CustomerId { get; set; }
    public int? Rating { get; set; }
    public string? Review { get; set; }
}

public class CustomerReviewViewModel : BaseViewModel
{
    public int CustomerId { get; set; }
    public virtual UserViewModel? Customer { get; set; }
    public int Rating { get; set; }
    public string? Review { get; set; }
}