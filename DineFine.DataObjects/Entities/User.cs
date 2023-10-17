using Microsoft.AspNetCore.Identity;

namespace DineFine.DataObjects.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}