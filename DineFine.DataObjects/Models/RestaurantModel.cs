using DineFine.DataObjects.Entities;

namespace DineFine.DataObjects.Models;

public class RestaurantCreateModel : BaseCreateModel
{
    public string Name { get; set; } = null!;
}

public class RestaurantUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; } 
}

public class RestaurantViewModel : BaseViewModel
{
    public string Name { get; set; } = null!;
    public virtual IList<MenuItemViewModel>? MenuItems { get; set; }
    public virtual IList<RestaurantCategoryViewModel>? Categories { get; set; }
    public virtual IList<RestaurantStockInfoViewModel>? StockInfo { get; set; }
    
}