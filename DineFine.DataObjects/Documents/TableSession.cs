using System.ComponentModel.DataAnnotations;
using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class TableSession
{
    [Key]
    public string TableSessionId { get; set; } = null!;
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public ICollection<Order> Orders { get; set; } = null!;
    [Required]
    public DateTime StartedAt { get; set; }
    [Required]
    public DateTime EndedAt { get; set; }
    [Required]
    public double TotalPrice { get; set; }
    [Required]
    public CustomerReviewViewModel? CustomerReview { get; set; }
}