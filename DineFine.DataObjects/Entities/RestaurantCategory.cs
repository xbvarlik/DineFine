namespace DineFine.DataObjects.Entities;

public class RestaurantCategory : BaseEntity
{
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public int RestaurantId { get; set; }
    
    public virtual List<MenuItem>? MenuItems { get; set; } 
}