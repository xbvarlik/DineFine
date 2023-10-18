using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class TableStatusMapper
{
    public static TableStatusViewModel ToViewModel(this TableStatus entity)
    {
        return new TableStatusViewModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }
    
    public static IEnumerable<TableStatusViewModel> ToTableStatusViewModelList(this IEnumerable<TableStatus> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}