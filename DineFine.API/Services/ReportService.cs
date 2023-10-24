using System.ComponentModel;
using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public class ReportService
{
    private readonly TableSessionService _tableSessionService;

    public ReportService(TableSessionService tableSessionService)
    {
        _tableSessionService = tableSessionService;
    }
    
    public ReportViewModel GetReport(ReportCreateModel createModel)
    {
        DateTime? endDate = createModel.ReportType switch
        {
            ReportType.Daily => null,
            ReportType.Weekly => createModel.StartDate.AddDays(7),
            ReportType.Monthly => createModel.StartDate.AddDays(30),
            ReportType.Yearly => createModel.StartDate.AddDays(365),
            _ => throw new InvalidEnumArgumentException()
        };
        
        var tableSessions = _tableSessionService.GetTableSessionsByDate(createModel.PartitionKey, createModel.StartDate, endDate);
        return CreateReport(tableSessions, createModel.ReportType);
    }
    
    private static ReportViewModel CreateReport(ICollection<TableSessionViewModel> tableSessions, string reportType)
    {
        var report = new ReportViewModel
        {
            ReportType = reportType,
            TotalSessionNumber = tableSessions.Count,
            Orders = new List<OrderViewModel>(),
            TotalRevenue = 0,
            CustomerReviews = new List<CustomerReviewViewModel?>()
        };
        
        foreach (var tableSessionViewModel in tableSessions)
        {
            report.TotalRevenue += tableSessionViewModel.TotalPrice;
            report.CustomerReviews.Add(tableSessionViewModel.CustomerReview);
            report.Orders.AddRange(tableSessionViewModel.Orders);
        }
        return report;
    }

}