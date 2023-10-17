namespace DineFine.DataObjects.Models;

public class MenuItemIngredientCreateModel : BaseCreateModel
{
    public int MenuItemId { get; set; }
    public int IngredientId { get; set; }
}

public class MenuItemIngredientUpdateModel : BaseUpdateModel
{
    public int? MenuItemId { get; set; }
    public int? IngredientId { get; set; }
}

public class MenuItemIngredientViewModel : BaseViewModel
{
    public int MenuItemId { get; set; }
    public virtual IngredientViewModel? Ingredient { get; set; }
}