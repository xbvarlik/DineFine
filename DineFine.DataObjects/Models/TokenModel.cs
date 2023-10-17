namespace DineFine.DataObjects.Models;

public class TokenModel
{
    public int UserId { get; set; }
    public string AccessToken { get; set; } = null!;
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiration { get; set; }
}