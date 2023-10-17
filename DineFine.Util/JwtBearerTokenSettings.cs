namespace DineFine.Util;

public class JwtBearerTokenSettings
{
    public IList<string> Audiences { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public int AccessTokenExpirationMinute { get; set; }
    public int RefreshTokenExpirationMinute { get; set; }
    public string SecurityKey { get; set; } = null!;
}