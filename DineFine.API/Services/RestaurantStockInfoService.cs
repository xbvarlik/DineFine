using DineFine.Accessor.DataAccessors.Mssql;
using DineFine.Accessor.Mappings;
using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace DineFine.API.Services;

public class RestaurantStockInfoService : BaseService<int, RestaurantStockInfo, RestaurantStockInfoViewModel, 
    RestaurantStockInfoCreateModel, RestaurantStockInfoUpdateModel, RestaurantStockInfoQueryFilterModel, MssqlContext>
{
    private readonly INotificationService _notificationService;
    public RestaurantStockInfoService(MssqlContext context, INotificationService notificationService) : base(context)
    {
        _notificationService = notificationService;
    }

    protected override IQueryable<RestaurantStockInfo> GetEntityDbSetAsQueryable()
    {
        return Context.RestaurantStockInfos
            .Include(x => x.Unit)
            .Include(x => x.Ingredient)
            .AsQueryable();
    }

    protected override Task<IEnumerable<RestaurantStockInfoViewModel>> OnAfterGetAllAsync(IEnumerable<RestaurantStockInfo> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToRestaurantStockInfoViewModelList());
    }

    protected override RestaurantStockInfoViewModel? OnAfterGet(RestaurantStockInfo? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<RestaurantStockInfo> OnBeforeCreateAsync(RestaurantStockInfoCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override Task<RestaurantStockInfoViewModel> OnAfterCreateAsync(RestaurantStockInfo entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }

    protected override async Task<RestaurantStockInfo> OnBeforeUpdateAsync(RestaurantStockInfo entity, RestaurantStockInfoUpdateModel updateModel,
        CancellationToken cancellationToken = default)
    {
        if (entity.Stock <= entity.Threshold)
            await _notificationService.OnStockLowAsync(entity.Ingredient!.Name);
        
        entity.ToUpdatedEntity(updateModel);
        return entity;
    }

    protected override Task<RestaurantStockInfoViewModel> OnAfterUpdateAsync(RestaurantStockInfo entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }
}