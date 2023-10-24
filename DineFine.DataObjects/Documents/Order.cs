using System.ComponentModel.DataAnnotations;
using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class Order
{
    [Key]
    [Required]
    public string OrderId { get; set; } = null!;

    [Required] public string RestaurantId { get; set; } = null!;
    [Required]
    public MenuItemCosmosViewModel MenuItem { get; set; } = null!;
    [Required]
    public OrderStatusViewModel OrderStatus { get; set; } = null!;
}