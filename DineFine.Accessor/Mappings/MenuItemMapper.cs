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
    
    public static MenuItemViewModel ToViewModel(this MenuItem entity, bool includeCategory = true)
    {
        if (!includeCategory)
            return new MenuItemViewModel
            {
                Name = entity.Name,
                Price = entity.Price,
                RestaurantId = entity.RestaurantId,
                Ingredients = entity.MenuItemIngredients?.ToMenuItemIngredientViewModelList()
            };
        
        return new MenuItemViewModel
        {
            Name = entity.Name,
            Price = entity.Price,
            RestaurantId = entity.RestaurantId,
            RestaurantCategory = entity.RestaurantCategory?.ToViewModel(),
            Ingredients = entity.MenuItemIngredients?.ToMenuItemIngredientViewModelList()
        };
    }

    public static MenuItemCosmosViewModel ToCosmosViewModel(this MenuItem entity)
    {
        return new MenuItemCosmosViewModel
        {
            Name = entity.Name,
            Price = entity.Price,
            CategoryName = entity.RestaurantCategory?.ToViewModel().Category?.Name ?? string.Empty,
            Ingredients = entity.MenuItemIngredients?.Select(x => x.Ingredient?.ToViewModel())
        };
    }
    
    public static IEnumerable<MenuItemViewModel> ToMenuItemViewModelList(this IEnumerable<MenuItem> entities, bool includeNavigationProperties = true)
    {
        return entities.Select(x => x.ToViewModel(includeNavigationProperties));
    }
}