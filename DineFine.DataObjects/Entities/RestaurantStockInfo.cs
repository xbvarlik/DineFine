using Microsoft.EntityFrameworkCore;

namespace DineFine.DataObjects.Entities;

public class RestaurantStockInfo : BaseEntity
{
    public double Stock { get; set; }
    public double Threshold { get; set; }
    public int RestaurantId { get; set; }
    public int UnitId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Unit? Unit { get; set; }
    public int IngredientId { get; set; }
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Ingredient? Ingredient { get; set; }
}

