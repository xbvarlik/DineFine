using Microsoft.AspNetCore.Identity;

namespace DineFine.DataObjects.Entities;

public class Role : IdentityRole<int>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}