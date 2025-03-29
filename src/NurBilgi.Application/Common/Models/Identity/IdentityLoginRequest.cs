namespace NurBilgi.Application.Common.Models.Identity;

public class IdentityLoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public IdentityLoginRequest(string email, string password, bool rememberMe = false)
    {
        Email = email;
        Password = password;
        RememberMe = rememberMe;
    }
}