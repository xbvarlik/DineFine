namespace DineFine.DataObjects.Entities;

public class AppRefreshToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpirationDate { get; set; }
}