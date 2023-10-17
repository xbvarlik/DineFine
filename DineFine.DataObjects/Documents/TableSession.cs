using DineFine.DataObjects.Models;

namespace DineFine.DataObjects.Documents;

public class TableSession
{
    public string TableSessionId { get; set; } = null!;
    public int RestaurantId { get; set; }
    public ICollection<Order> Oders { get; set; } = null!;
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public double TotalPrice { get; set; }
    public CustomerReviewViewModel? CustomerReview { get; set; }
}