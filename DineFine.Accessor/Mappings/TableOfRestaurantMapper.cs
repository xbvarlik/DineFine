using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class TableOfRestaurantMapper
{
    public static TableOfRestaurant ToEntity(this TableOfRestaurantCreateModel model)
    {
        return new TableOfRestaurant
        {
            RestaurantId = model.RestaurantId,
            TableStatusId = 1,
            NumberOfSeats = model.NumberOfSeats
        };
    } 
    
    public static void ToUpdatedEntity(this TableOfRestaurant entity, TableOfRestaurantUpdateModel model)
    {
        entity.RestaurantId = model.RestaurantId ?? entity.RestaurantId;
        entity.TableStatusId = model.TableStatusId ?? entity.TableStatusId;
        entity.NumberOfSeats = model.NumberOfSeats ?? entity.NumberOfSeats;
    }
    
    public static TableOfRestaurantViewModel ToViewModel(this TableOfRestaurant entity)
    {
        return new TableOfRestaurantViewModel
        {
            Id = entity.Id,
            RestaurantId = entity.RestaurantId,
            TableStatus = entity.TableStatus?.ToViewModel(),
            NumberOfSeats = entity.NumberOfSeats
        };
    }
    
    public static IEnumerable<TableOfRestaurantViewModel> ToTableOfRestaurantViewModelList(this IEnumerable<TableOfRestaurant> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}