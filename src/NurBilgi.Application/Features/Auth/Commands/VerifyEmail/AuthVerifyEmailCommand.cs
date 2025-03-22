using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.VerifyEmail;

public class AuthVerifyEmailCommand : IRequest<ResponseDto<string>>
{
    public string Email { get; set; }
    public string Token { get; set; }

    public AuthVerifyEmailCommand(string email, string token)
    {
        Email = email;
        Token = token;
    }

}