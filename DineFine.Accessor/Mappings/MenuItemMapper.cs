using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class MenuItemMapper
{
    public static MenuItem ToEntity(this MenuItemCreateModel model)
    {
        // Image should be handled in service
        return new MenuItem
        {
            Name = model.Name,
            Price = model.Price,
            RestaurantCategoryId = model.CategoryId,
            RestaurantId = model.RestaurantId
        };
    } 
    
    public static void ToUpdatedEntity(this MenuItem entity, MenuItemUpdateModel model)
    {
        entity.Name = model.Name ?? entity.Name;
        entity.Price = model.Price ?? entity.Price;
        entity.RestaurantCategoryId = model.CategoryId ?? entity.RestaurantCategoryId;
        entity.RestaurantId = model.RestaurantId ?? entity.RestaurantId;
    }
    
    public static MenuItemViewModel ToViewModel(this MenuItem entity, bool includeNavigationProperties = true)
    {
        if (!includeNavigationProperties)
            return new MenuItemViewModel
            {
                Name = entity.Name,
                Price = entity.Price,
                RestaurantId = entity.RestaurantId,
            };
        
        return new MenuItemViewModel
        {
            Name = entity.Name,
            Price = entity.Price,
            RestaurantId = entity.RestaurantId,
            Category = entity.RestaurantCategory?.ToViewModel(),
            Ingredients = entity.MenuItemIngredients?.ToMenuItemIngredientViewModelList()
        };
    }
    
    public static IEnumerable<MenuItemViewModel> ToMenuItemViewModelList(this IEnumerable<MenuItem> entities, bool includeNavigationProperties = true)
    {
        return entities.Select(x => x.ToViewModel(includeNavigationProperties));
    }
}