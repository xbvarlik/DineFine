using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.API.Attributes;
using DineFine.API.Result;
using DineFine.API.Services;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace DineFine.API.Controllers;

[SpecificAccess("RestaurantOwner")]
public class TableOfRestaurantController : BaseController<int, TableOfRestaurant, TableOfRestaurantViewModel, 
    TableOfRestaurantCreateModel, TableOfRestaurantUpdateModel, BaseQueryFilterModel, MssqlContext>
{
    private readonly TableSessionCacheService _tableSessionCacheService;
    
    public TableOfRestaurantController(TableOfRestaurantService service, TableSessionCacheService tableSessionCacheService) : base(service)
    {
        _tableSessionCacheService = tableSessionCacheService;
    }

    [SpecificAccess("Waiter, Customer")]
    public override async Task<IActionResult> UpdateAsync(int id, TableOfRestaurantUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        var result = await Service.UpdateAsync(id, updateModel, cancellationToken);
        
        await CheckTableStatus(result);
        
        return ApiResult.CreateActionResult(ServiceResult<TableOfRestaurantViewModel>.Success(204, result));
    }
    
    private async Task CheckTableStatus(TableOfRestaurantViewModel table)
    {
        switch (table.TableStatus!.Id)
        {
            case 1:
                await _tableSessionCacheService.KillTableSessionAsync();
                break;
            case 2:
                _tableSessionCacheService.CreateTableSession(table);
                break;
            case 3:
                break;
        }
    }
}