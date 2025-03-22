namespace NurBilgi.Application.Common.Models.Identity;

public class IdentityRegisterResponse
{
    public long Id { get; set; }

    public string Email { get; set; }

    public string EmailToken { get; set; }

    public IdentityRegisterResponse(long id, string email, string emailToken)
    {
        Id = id;
        Email = email;
        EmailToken = emailToken;
    }
}