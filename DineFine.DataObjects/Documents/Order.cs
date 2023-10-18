using System.ComponentModel.DataAnnotations;
using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class Order
{
    [Key]
    [Required]
    public string OrderId { get; set; } = null!;
    [Required]
    public int RestaurantId { get; set; }
    public CustomerReviewViewModel? CustomerReview { get; set; }
    [Required]
    public RestaurantViewModel Restaurant { get; set; } = null!;
    [Required]
    public MenuItemViewModel MenuItem { get; set; } = null!;
    [Required]
    public TableOfRestaurantViewModel TableOfRestaurant { get; set; } = null!;
    [Required]
    public OrderStatusViewModel OrderStatus { get; set; } = null!;
}