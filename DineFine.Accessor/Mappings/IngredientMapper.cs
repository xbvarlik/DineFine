using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class IngredientMapper
{
    public static Ingredient ToEntity(this IngredientCreateModel model)
    {
        return new Ingredient
        {
            Name = model.Name
        };
    } 
    
    public static void ToUpdatedEntity(this Ingredient entity, IngredientUpdateModel model)
    {
        entity.Name = model.Name ?? entity.Name;
    }
    
    public static IngredientViewModel ToViewModel(this Ingredient entity)
    {
        return new IngredientViewModel
        {
            Name = entity.Name
        };
    }
    
    public static IEnumerable<IngredientViewModel> ToIngredientViewModelList(this IEnumerable<Ingredient> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}