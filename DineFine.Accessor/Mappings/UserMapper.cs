using DineFine.DataObjects.Entities;
using DineFine.DataObjects.Models;

namespace DineFine.Accessor.Mappings;

public static class UserMapper
{
    public static UserViewModel ToViewModel(this User entity)
    {
        return new UserViewModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
        };
    }
    
    public static IEnumerable<UserViewModel> ToViewModelList(this IEnumerable<User> entities)
    {
        return entities.Select(ToViewModel).ToList();
    }
}