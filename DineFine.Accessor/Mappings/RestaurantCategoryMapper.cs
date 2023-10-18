using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class RestaurantCategoryMapper
{
    public static RestaurantCategory ToEntity(this RestaurantCategoryCreateModel model)
    {
        return new RestaurantCategory
        {
            RestaurantId = model.RestaurantId,
            CategoryId = model.CategoryId
        };
    } 
    
    public static void ToUpdatedEntity(this RestaurantCategory entity, RestaurantCategoryUpdateModel model)
    {
        entity.RestaurantId = model.RestaurantId ?? entity.RestaurantId;
        entity.CategoryId = model.CategoryId ?? entity.CategoryId;
    }
    
    public static RestaurantCategoryViewModel ToViewModel(this RestaurantCategory entity, bool includeMenuItems = true)
    {
        if(!includeMenuItems)
            return new RestaurantCategoryViewModel
            {
                Id = entity.Id, 
                RestaurantId = entity.RestaurantId,
                Category = entity.Category?.ToViewModel()
            };

        return new RestaurantCategoryViewModel
        {
            Id = entity.Id, 
            RestaurantId = entity.RestaurantId,
            Category = entity.Category?.ToViewModel(),
            MenuItems = entity.MenuItems?.ToMenuItemViewModelList(false)
        };
    }
    
    public static IEnumerable<RestaurantCategoryViewModel> ToRestaurantCategoryViewModelList(this IEnumerable<RestaurantCategory> 
        entities, bool includeMenuItems = true)
    {
        return entities.Select(x => x.ToViewModel(includeMenuItems));
    }
}