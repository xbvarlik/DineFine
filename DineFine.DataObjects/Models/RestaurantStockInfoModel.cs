namespace DineFine.DataObjects.Models;

public class RestaurantStockInfoCreateModel : BaseCreateModel
{   
    public double Stock { get; set; }
    public int RestaurantId { get; set; }
    public int UnitId { get; set; }
    public int IngredientId { get; set; }
}

public class RestaurantStockInfoUpdateModel : BaseUpdateModel
{
    public double? Stock { get; set; }
    public int? RestaurantId { get; set; }
    public int? UnitId { get; set; }
    public int? IngredientId { get; set; }
}

public class RestaurantStockInfoViewModel : BaseViewModel
{
    public double Stock { get; set; }
    public int RestaurantId { get; set; }
    public virtual UnitViewModel? Unit { get; set; }
    public virtual IngredientViewModel? Ingredient { get; set; }
}

public class RestaurantStockInfoQueryFilterModel : BaseQueryFilterModel
{
    public string? IngredientName { get; set; }
}