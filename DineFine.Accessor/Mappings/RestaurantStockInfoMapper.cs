using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class RestaurantStockInfoMapper
{
    public static RestaurantStockInfo ToEntity(this RestaurantStockInfoCreateModel model)
    {
        return new RestaurantStockInfo
        {
            Stock = model.Stock,
            UnitId = model.UnitId,
            IngredientId = model.IngredientId
        };
    } 
    
    public static void ToUpdatedEntity(this RestaurantStockInfo entity, RestaurantStockInfoUpdateModel model)
    {
        entity.Stock = model.Stock ?? entity.Stock;
        entity.UnitId = model.UnitId ?? entity.UnitId;
        entity.IngredientId = model.IngredientId ?? entity.IngredientId;
    }
    
    public static RestaurantStockInfoViewModel ToViewModel(this RestaurantStockInfo entity, bool includeNavigationProperties = true)
    {
        if (!includeNavigationProperties)
            return new RestaurantStockInfoViewModel
            {
                Id = entity.Id,
                Stock = entity.Stock,
                RestaurantId = entity.RestaurantId,
            };
        
        return new RestaurantStockInfoViewModel
        {
            Id = entity.Id,
            Stock = entity.Stock,
            RestaurantId = entity.RestaurantId,
            Unit = entity.Unit?.ToViewModel(),
            Ingredient = entity.Ingredient?.ToViewModel()
        };
    }
    
    public static IEnumerable<RestaurantStockInfoViewModel> ToRestaurantStockInfoViewModelList(this IEnumerable<RestaurantStockInfo> entities, 
        bool includeNavigationProperties = true)
    {
        return entities.Select(x => x.ToViewModel(includeNavigationProperties));
    }
}