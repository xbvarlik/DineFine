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
    public virtual IEnumerable<MenuItemViewModel>? MenuItems { get; set; }
    public virtual IEnumerable<TableOfRestaurantViewModel>? Tables { get; set; }
    public virtual IEnumerable<RestaurantCategoryViewModel>? Categories { get; set; }
}

public class RestaurantCosmosViewModel : BaseViewModel
{
    public string Name { get; set; } = null!;
}