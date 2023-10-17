namespace DineFine.DataObjects.Entities;

public class RestaurantCategory : BaseEntity
{
    public int CategoryId { get; set; }
    public int RestaurantId { get; set; }
}