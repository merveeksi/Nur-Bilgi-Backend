namespace NurBilgi.Application.Common.Models.Email;

public sealed class EmailVerificationDto
{
    public string Email { get; set; }
    public string Token { get; set; }

    public EmailVerificationDto(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public EmailVerificationDto()
    {

    }
}