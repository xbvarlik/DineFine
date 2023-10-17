namespace DineFine.DataObjects.Entities;

public class Ingredient : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<MenuItemIngredient>? MenuItemIngredients { get; set; }
    public virtual ICollection<RestaurantStockInfo>? RestaurantStockInfos { get; set; }
}