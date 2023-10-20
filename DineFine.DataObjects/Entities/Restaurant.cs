using Microsoft.EntityFrameworkCore;

namespace DineFine.DataObjects.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; } = null!;
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual ICollection<MenuItem>? MenuItems { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual ICollection<RestaurantCategory>? RestaurantCategories { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual ICollection<RestaurantStockInfo>? RestaurantStockInfos { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual ICollection<TableOfRestaurant>? TablesOfRestaurant { get; set; }
}