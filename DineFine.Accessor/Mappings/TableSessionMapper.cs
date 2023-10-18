using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class TableSessionMapper
{
    public static TableSession ToEntity(this TableSessionCreateModel model)
    {
        return new TableSession
        {
            RestaurantId = model.RestaurantId,
            Restaurant = model.Restaurant.ToEntity().ToViewModel(false),
            Orders = model.Orders.Select(x => x.ToEntity().ToViewModel()),
            TableOfRestaurant = model.TableOfRestaurant.ToEntity().ToViewModel(),
            StartedAt = model.StartedAt,
            EndedAt = model.EndedAt,
            TotalPrice = model.TotalPrice,
            CustomerReview = model.CustomerReview.ToViewModel()
        };
    }

    public static void ToUpdatedEntity(this TableSession entity, TableSessionUpdateModel model)
    {
        entity.Orders = model.Orders?.Select(x => x.ToEntity().ToViewModel()) ?? entity.Orders;
        entity.TableOfRestaurant = model.TableOfRestaurant?.ToEntity().ToViewModel() ?? entity.TableOfRestaurant;
        entity.StartedAt = model.StartedAt ?? entity.StartedAt;
        entity.EndedAt = model.EndedAt ?? entity.EndedAt;
        entity.TotalPrice = model.TotalPrice ?? entity.TotalPrice;
        entity.CustomerReview.ToUpdatedViewModel(model.CustomerReview);
    }
    
    public static TableSessionViewModel ToViewModel(this TableSession entity)
    {
        return new TableSessionViewModel
        {
            Restaurant = entity.Restaurant,
            TableOfRestaurant = entity.TableOfRestaurant,
            Orders = entity.Orders,
            StartedAt = entity.StartedAt,
            EndedAt = entity.EndedAt,
            TotalPrice = entity.TotalPrice,
            CustomerReview = entity.CustomerReview 
        };
    }
    
    public static IEnumerable<TableSessionViewModel> ToTableSessionViewModelList(this IEnumerable<TableSession> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}