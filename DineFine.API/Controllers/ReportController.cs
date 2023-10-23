using DineFine.API.Attributes;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[SpecificAccess("SuperAdmin, RestaurantOwner")]
public class ReportController
{
    private readonly ReportService _reportService;

    public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }
    
    [HttpGet]
    public IActionResult GetReport([FromQuery] ReportCreateModel createModel)
    {
        var response= _reportService.GetReport(createModel);
        return ApiResult.CreateActionResult(ServiceResult<ReportViewModel>.Success(200, response));
    }
}