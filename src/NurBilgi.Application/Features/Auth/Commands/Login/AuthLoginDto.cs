using System.ComponentModel.DataAnnotations;
using NurBilgi.Application.Common.Models.Identity;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public class AuthLoginDto
{
    public string Token { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    public AuthLoginDto()
    {
        
    }

    public AuthLoginDto(string token, DateTimeOffset expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public static AuthLoginDto FromIdentityLoginResponse(IdentityLoginResponse response)
    {
        return new AuthLoginDto(response.Token, response.ExpiresAt);
    }
}
