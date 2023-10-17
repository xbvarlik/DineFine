namespace DineFine.DataObjects.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<MenuItem>? MenuItems { get; set; }
    public virtual ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
    public virtual ICollection<RestaurantStockInfo>? RestaurantStockInfos { get; set; }
    public virtual ICollection<TableOfRestaurant>? TablesOfRestaurant { get; set; }
}