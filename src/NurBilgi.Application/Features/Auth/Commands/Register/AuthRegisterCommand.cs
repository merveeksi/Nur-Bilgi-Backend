using MediatR;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Auth.Commands.Register;

public sealed record AuthRegisterCommand : IRequest<ResponseDto<AuthRegisterDto>>
{
    public string Email { get; set; }

    public string Password { get; set; }
    
    public FullName? FullName { get; set; }

    public IdentityRegisterRequest ToIdentityRegisterRequest()
    {
        return new IdentityRegisterRequest(Email, Password, FullName);
    }
}