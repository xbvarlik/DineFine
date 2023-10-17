using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class UnitMapper
{
    public static UnitViewModel ToViewModel(this Unit entity)
    {
        return new UnitViewModel
        {
            Name = entity.Name
        };
    }
    
    public static IEnumerable<UnitViewModel> ToUnitViewModelList(this IEnumerable<Unit> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}