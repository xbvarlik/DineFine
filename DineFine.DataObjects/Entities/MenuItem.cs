using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DineFine.DataObjects.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public double Price { get; set; }
    
    public string Image { get; set; } = null!;
    public int RestaurantCategoryId { get; set; }
    
    public int RestaurantId { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("RestaurantCategoryId")]
    public virtual RestaurantCategory? RestaurantCategory { get; set; } 
    
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("RestaurantId")]
    public virtual Restaurant? Restaurant { get; set; } 
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual ICollection<MenuItemIngredient>? MenuItemIngredients { get; set; }
}