namespace DineFine.DataObjects.Entities;

public class MenuItemIngredient : BaseEntity
{
    public int MenuItemId { get; set; }
    public int IngredientId { get; set; }
    public virtual Ingredient? Ingredient { get; set; }
}