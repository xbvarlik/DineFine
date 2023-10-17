namespace DineFine.DataObjects.Models;

public class RestaurantCategoryCreateModel : BaseCreateModel 
{
    public int RestaurantId { get; set; }
    public int CategoryId { get; set; }
}

public class RestaurantCategoryUpdateModel : BaseUpdateModel
{
    public int? RestaurantId { get; set; }
    public int? CategoryId { get; set; }
}

public class RestaurantCategoryViewModel: BaseViewModel
{
    public int RestaurantId { get; set; }
    public int CategoryId { get; set; }
    public virtual CategoryViewModel? Category { get; set; }
}

public class RestaurantCategoryQueryFilterModel : BaseQueryFilterModel
{
    public string? CategoryName { get; set; }
}