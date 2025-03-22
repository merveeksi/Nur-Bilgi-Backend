namespace NurBilgi.Application.Common.Models.Identity;

public class IdentityCreateEmailTokenRequest
{
    public string Email { get; set; }

    public IdentityCreateEmailTokenRequest(string email)
    {
        Email = email;
    }
}