namespace DineFine.DataObjects.Models;

public class TableOfRestaurantCreateModel : BaseCreateModel
{
    public int RestaurantId { get; set; }
    public int NumberOfSeats { get; set; }
}

public class TableOfRestaurantUpdateModel : BaseUpdateModel
{
    public int? RestaurantId { get; set; }
    public int? TableStatusId { get; set; }
    public int? NumberOfSeats { get; set; }
}

public class TableOfRestaurantViewModel : BaseViewModel
{
    public int RestaurantId { get; set; }
    public virtual TableStatusViewModel? TableStatus { get; set; }
    public virtual RestaurantCosmosViewModel? Restaurant { get; set; }
    public int NumberOfSeats { get; set; }
}
