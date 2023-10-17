namespace DineFine.DataObjects.Entities;

public class Unit : BaseEntity
{
    public string Name { get; set; } = null!;
}

public class MenuItem : BaseEntity
{
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public string Image { get; set; } = null!;
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; } 
    public int RestaurantId { get; set; }
    public virtual Restaurant? Restaurant { get; set; } 
    public virtual ICollection<MenuItemIngredient>? MenuItemIngredients { get; set; }
}