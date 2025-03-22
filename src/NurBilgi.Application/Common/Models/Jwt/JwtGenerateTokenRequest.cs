namespace NurBilgi.Application.Common.Models.Jwt;

public class JwtGenerateTokenRequest
{
    public long Id { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }

    public JwtGenerateTokenRequest(long id, string email, IList<string> roles)
    {
        Id = id;

        Email = email;

        Roles = roles;
    }
}