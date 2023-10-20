using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class RestaurantMapper
{
    public static Restaurant ToEntity(this RestaurantCreateModel model)
    {
        return new Restaurant
        {
            Name = model.Name
        };
    } 
    
    public static void ToUpdatedEntity(this Restaurant entity, RestaurantUpdateModel model)
    {
        entity.Name = model.Name ?? entity.Name;
    }
    
    public static RestaurantViewModel ToViewModel(this Restaurant entity, bool includeNavigationProperties = true)
    {
        if(!includeNavigationProperties)
            return new RestaurantViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };
        
        return new RestaurantViewModel
        {
            Id = entity.Id,
            Name = entity.Name,
            MenuItems = entity.MenuItems?.ToMenuItemViewModelList(false),
            Categories = entity.RestaurantCategories?.ToRestaurantCategoryViewModelList(false),
            Tables = entity.TablesOfRestaurant?.ToTableOfRestaurantViewModelList(),
        };
    }
    
    public static IEnumerable<RestaurantViewModel> ToRestaurantViewModelList(this IEnumerable<Restaurant> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}