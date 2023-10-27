using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.API.Attributes;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner, KitchenPersonnel, Waiter")]
public class OrderController : BaseCosmosController<Order, OrderViewModel, OrderCreateModel, OrderUpdateModel, 
    BaseCosmosQueryFilterModel>
{
    private readonly TableSessionCacheService _tableSessionCacheService;
    
    public OrderController(OrderService service, TableSessionCacheService tableSessionCacheService) : base(service)
    {
        _tableSessionCacheService = tableSessionCacheService;
    }

    public override async Task<IActionResult> CreateAsync(OrderCreateModel createModel, CancellationToken cancellationToken = default)
    {
        var result = await Service.CreateAsync(createModel, cancellationToken);
        
        await AddOrderToTableSession(result);
        
        return ApiResult.CreateActionResult(ServiceResult<OrderViewModel>.Success(201, result));
    }

    private async Task AddOrderToTableSession(OrderViewModel order)
    {
        var tableSession = await _tableSessionCacheService.GetTableSessionAsync();

        if (tableSession == null) return;
        
        tableSession.Orders.Add(order);
        _tableSessionCacheService.UpdateTableSession(tableSession);
    }
}