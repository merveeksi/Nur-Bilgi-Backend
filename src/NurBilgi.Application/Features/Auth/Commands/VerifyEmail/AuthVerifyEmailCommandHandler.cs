using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.VerifyEmail;

public class AuthVerifyEmailCommandHandler : IRequestHandler<AuthVerifyEmailCommand, ResponseDto<string>>
{
    private readonly IIdentityService _identityService;

    public AuthVerifyEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ResponseDto<string>> Handle(AuthVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.VerifyEmailAsync(new IdentityVerifyEmailRequest(request.Email, request.Token), cancellationToken);

        return new ResponseDto<string>(data: response.Email, message: "Email verified successfully.");
    }
}