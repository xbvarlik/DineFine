using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DineFine.DataObjects.Models;

public class ReportCreateModel
{
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public string ReportType { get; set; } = null!;
}

public class ReportViewModel
{
    [Required]
    public string ReportType { get; set; } = null!;
    public int TotalSessionNumber { get; set; }
    public List<OrderViewModel>? Orders { get; set; }
    public double TotalRevenue { get; set; }
    public IList<CustomerReviewViewModel?>? CustomerReviews { get; set; }
}

public abstract class ReportType
{
    public const string Daily = "Daily";
    public const string Weekly = "Weekly";
    public const string Monthly = "Monthly";
    public const string Yearly = "Yearly";
}