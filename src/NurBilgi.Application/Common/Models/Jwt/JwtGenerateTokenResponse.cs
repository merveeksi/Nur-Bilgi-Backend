namespace NurBilgi.Application.Common.Models.Jwt;

public class JwtGenerateTokenResponse
{
    public string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public JwtGenerateTokenResponse(string token, DateTimeOffset expiresAt)
    {
        Token = token;

        ExpiresAt = expiresAt;
    }
}