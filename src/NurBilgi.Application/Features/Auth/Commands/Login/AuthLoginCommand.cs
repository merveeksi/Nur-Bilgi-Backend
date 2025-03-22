using MediatR;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public sealed record AuthLoginCommand : IRequest<ResponseDto<AuthLoginDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    
    public AuthLoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public IdentityLoginRequest ToIdentityLoginRequest()
    {
        return new IdentityLoginRequest(Email, Password);
    }

    public IdentityAuthenticateRequest ToIdentityAuthenticateRequest()
    {
        return new IdentityAuthenticateRequest(Email, Password);
    }
}