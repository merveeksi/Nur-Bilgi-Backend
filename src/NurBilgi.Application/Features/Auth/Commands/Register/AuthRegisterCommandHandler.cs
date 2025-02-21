using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.Register;

public sealed class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, ResponseDto<string>>
{
    private readonly IIdentityService _identityService;
    public AuthRegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<ResponseDto<string>> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
    {
        var userId = await _identityService.RegisterAsync(request, cancellationToken);

        return ResponseDto<string>.Success(userId, "User registered successfully");
    }
}
