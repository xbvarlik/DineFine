using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class RoleMapper
{
    public static Role ToEntity(this RoleCreateModel model)
    {
        return new Role
        {
            Name = model.Name
        };
    } 
    
    public static void ToUpdatedEntity(this Role entity, RoleUpdateModel model)
    {
        entity.Name = model.Name ?? entity.Name;
    }
    
    public static RoleViewModel ToViewModel(this Role entity)
    {
        return new RoleViewModel
        {
            Id = entity.Id,
            Name = entity.Name!
        };
    }
    
    public static IEnumerable<RoleViewModel> ToRoleViewModelList(this IEnumerable<Role> entities)
    {
        return entities.Select(x => x.ToViewModel());
    }
}