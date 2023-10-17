namespace DineFine.DataObjects.Documents;

public class UserSession<TTokenModel>
{
    public string UserSessionId { get; set; } = null!;
    public int UserId { get; set; }
    public string Agent { get; set; } = null!;
    public string Email { get; set; } = null!;
    public IList<string> UserRoles { get; set; } = null!;
    public TTokenModel LoginInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}