namespace NurBilgi.Application.Common.Models.Identity;

public class IdentityCreateEmailTokenResponse
{
    public string Token { get; set; }

    public IdentityCreateEmailTokenResponse(string token)
    {
        Token = token;
    }
}