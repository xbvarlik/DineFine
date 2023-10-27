using System.ComponentModel.DataAnnotations;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class TableSession
{
    [Key]
    public string TableSessionId { get; set; } = null!;
    [Required] 
    public string RestaurantId { get; set; } = null!;
    public RestaurantCosmosViewModel Restaurant { get; set; } = null!;
    public TableOfRestaurantViewModel TableOfRestaurant { get; set; } = null!;
    [Required]
    public IList<OrderViewModel> Orders { get; set; } = null!;
    [Required]
    public DateTime StartedAt { get; set; }
    [Required]
    public DateTime EndedAt { get; set; }
    [Required]
    public double TotalPrice { get; set; }
    public CustomerReviewViewModel? CustomerReview { get; set; }
}