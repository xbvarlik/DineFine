namespace DineFine.DataObjects.Entities;

public class RestaurantStockInfo : BaseEntity
{
    public double Stock { get; set; }
    public int RestaurantId { get; set; }
    public int UnitId { get; set; }
    public virtual Unit? Unit { get; set; }
    public int IngredientId { get; set; }
    public virtual Ingredient? Ingredient { get; set; }
}

