namespace NurBilgi.Application.Common.Models.Identity;

public class IdentityLoginResponse
{
    public string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public IdentityLoginResponse(string token, DateTimeOffset expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }
}