using System.ComponentModel.DataAnnotations;

namespace DineFine.DataObjects.Documents;

public class UserSession<TTokenModel>
{
    [Key]
    public string UserSessionId { get; set; } = null!;
    [Required]
    public int UserId { get; set; }
    [Required]
    public int TenantId { get; set; }
    [Required]
    public string Agent { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    public IList<string> UserRoles { get; set; } = null!;
    [Required] 
    public virtual TTokenModel? LoginInfo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}