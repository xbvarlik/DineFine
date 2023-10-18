using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class MenuItemIngredientMapper
{
    public static MenuItemIngredient ToEntity(this MenuItemIngredientCreateModel model)
    {
        return new MenuItemIngredient
        {
            MenuItemId = model.MenuItemId,
            IngredientId = model.IngredientId
        };
    } 
    
    public static void ToUpdatedEntity(this MenuItemIngredient entity, MenuItemIngredientUpdateModel model)
    {
        entity.MenuItemId = model.MenuItemId ?? entity.MenuItemId;
        entity.IngredientId = model.IngredientId ?? entity.IngredientId;
    }
    
    public static MenuItemIngredientViewModel ToViewModel(this MenuItemIngredient entity)
    {
        return new MenuItemIngredientViewModel
        {
            MenuItemId = entity.MenuItemId,
            Ingredient = entity.Ingredient?.ToViewModel()
        };
    }
    
    public static IEnumerable<MenuItemIngredientViewModel> ToMenuItemIngredientViewModelList(this IEnumerable<MenuItemIngredient> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}