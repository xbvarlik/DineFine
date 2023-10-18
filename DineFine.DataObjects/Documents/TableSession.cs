using System.ComponentModel.DataAnnotations;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class TableSession
{
    [Key]
    public string TableSessionId { get; set; } = null!;
    [Required]
    public int RestaurantId { get; set; }
    public RestaurantViewModel Restaurant { get; set; } = null!;
    public TableOfRestaurantViewModel TableOfRestaurant { get; set; } = null!;
    [Required]
    public IEnumerable<OrderViewModel> Orders { get; set; } = null!;
    [Required]
    public DateTime StartedAt { get; set; }
    [Required]
    public DateTime EndedAt { get; set; }
    [Required]
    public double TotalPrice { get; set; }
    [Required]
    public CustomerReviewViewModel? CustomerReview { get; set; }
}