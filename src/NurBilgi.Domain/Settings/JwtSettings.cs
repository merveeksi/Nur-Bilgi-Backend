namespace NurBilgi.Domain.Settings;

public record JwtSettings
{
    public string SecretKey { get; set; }
    public TimeSpan TokenExpiration { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}