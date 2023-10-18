using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class OrderStatusMapper
{
    public static OrderStatusViewModel ToViewModel(this OrderStatus entity)
    {
        return new OrderStatusViewModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static IEnumerable<OrderStatusViewModel> ToOrderStatusViewModelList(this IEnumerable<OrderStatus> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}