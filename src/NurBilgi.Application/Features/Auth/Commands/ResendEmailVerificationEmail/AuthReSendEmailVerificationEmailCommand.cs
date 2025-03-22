using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.ResendEmailVerificationEmail;

public class AuthReSendEmailVerificationEmailCommand : IRequest<ResponseDto<string>>
{
    public string Email { get; set; }

    public AuthReSendEmailVerificationEmailCommand(string email)
    {
        Email = email;
    }

}