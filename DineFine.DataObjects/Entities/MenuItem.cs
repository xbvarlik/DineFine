namespace DineFine.DataObjects.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public string Image { get; set; } = null!;
    public int RestaurantCategoryId { get; set; }
    public virtual RestaurantCategory? RestaurantCategory { get; set; } 
    public int RestaurantId { get; set; }
    public virtual Restaurant? Restaurant { get; set; } 
    public virtual ICollection<MenuItemIngredient>? MenuItemIngredients { get; set; }
}