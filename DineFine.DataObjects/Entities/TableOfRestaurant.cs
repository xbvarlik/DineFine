namespace DineFine.DataObjects.Entities;

public class TableOfRestaurant : BaseEntity
{
    public int RestaurantId { get; set; }
    public int TableStatusId { get; set; }
    public virtual TableStatus? TableStatus { get; set; }
    public int NumberOfSeats { get; set; }
}