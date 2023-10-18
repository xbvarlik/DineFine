namespace DineFine.DataObjects.Models;

public class MenuItemCreateModel : BaseCreateModel
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int RestaurantId { get; set; }
    public int CategoryId { get; set; }
}

public class MenuItemUpdateModel : BaseUpdateModel
{
    public string? Name { get; set; }
    public double? Price { get; set; }
    public int? RestaurantId { get; set; }
    public int? CategoryId { get; set; }
}

public class MenuItemViewModel : BaseViewModel
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public int RestaurantId { get; set; }
    public virtual RestaurantCategoryViewModel? Category { get; set; }
    public virtual IEnumerable<MenuItemIngredientViewModel>? Ingredients { get; set; }
}

public class MenuItemQueryFilterModel : BaseQueryFilterModel
{
    public string? Name { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
}