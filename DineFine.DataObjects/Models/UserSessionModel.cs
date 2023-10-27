using DineFine.DataObjects.Documents;
using DineFine.DataObjects.Entities;

namespace DineFine.DataObjects.Models;

public class UserSessionCreateModel
{
    public virtual User User { get; set; } = null!;
    public IList<string>? UserRoles { get; set; }
    public TokenModel TokenModel { get; set; } = null!;
    public string Agent { get; set; } = null!;
}

public class UserSessionUpdateModel
{
    public string UserId { get; set; } = null!;

    public int TenantId { get; set; }

    public string Agent { get; set; } = null!;

    public string Email { get; set; } = null!;

    public IList<string> UserRoles { get; set; } = null!;

    public virtual TokenModel? LoginInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}