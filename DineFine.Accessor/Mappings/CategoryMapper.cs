using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class CategoryMapper
{
    public static Category ToEntity(this CategoryCreateModel model)
    {
        return new Category
        {
            Name = model.Name
        };
    } 
    
    public static void ToUpdatedEntity(this Category entity, CategoryUpdateModel model)
    {
        entity.Name = model.Name ?? entity.Name;
    }
    
    public static CategoryViewModel ToViewModel(this Category entity)
    {
        return new CategoryViewModel
        {
            Name = entity.Name
        };
    }
    
    public static IEnumerable<CategoryViewModel> ToCategoryViewModelList(this IEnumerable<Category> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}