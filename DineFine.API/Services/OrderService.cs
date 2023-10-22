using DineFine.Accessor.DataAccessors.Cosmos;
using DineFine.Accessor.Mappings;
using DineFine.API.Constants;
using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public class OrderService : BaseCosmosService<Order, OrderViewModel, OrderCreateModel, OrderUpdateModel, 
    BaseCosmosQueryFilterModel>
{
    private readonly INotificationService _notificationService;
    
    public OrderService(CosmosContext context, INotificationService notificationService) : base(context)
    {
        _notificationService = notificationService;
    }

    protected override Task<IEnumerable<OrderViewModel>> OnAfterGetAllAsync(IEnumerable<Order> entities, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entities.ToViewModelList());
    }

    protected override OrderViewModel? OnAfterGet(Order? entity, CancellationToken cancellationToken = default)
    {
        return entity?.ToViewModel();
    }

    protected override Task<Order> OnBeforeCreateAsync(OrderCreateModel createModel, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(createModel.ToEntity());
    }

    protected override async Task<OrderViewModel> OnAfterCreateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        await _notificationService.OnOrderReceivedAsync(entity.OrderId);
        
        return entity.ToViewModel();
    }

    protected override async Task<Order> OnBeforeUpdateAsync(Order entity, OrderUpdateModel updateModel, CancellationToken cancellationToken = default)
    {
        if (entity.OrderStatus.Id == OrderStatusCodes.Ready) 
            await _notificationService.OnOrderReadyAsync(entity.OrderId);
        
        entity.ToUpdatedEntity(updateModel);
        return entity;
    }

    protected override Task<OrderViewModel> OnAfterUpdateAsync(Order entity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(entity.ToViewModel());
    }


}