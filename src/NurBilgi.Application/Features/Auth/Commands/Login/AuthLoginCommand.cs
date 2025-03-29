using MediatR;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public sealed record AuthLoginCommand : IRequest<ResponseDto<AuthLoginDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    
    // Boş constructor eklendi (model binding için)
    public AuthLoginCommand()
    {
    }
    
    public AuthLoginCommand(string email, string password, bool rememberMe = false)
    {
        Email = email;
        Password = password;
        RememberMe = rememberMe;
    }

    public IdentityLoginRequest ToIdentityLoginRequest()
    {
        return new IdentityLoginRequest(Email, Password, RememberMe);
    }

    public IdentityAuthenticateRequest ToIdentityAuthenticateRequest()
    {
        return new IdentityAuthenticateRequest(Email, Password);
    }
}