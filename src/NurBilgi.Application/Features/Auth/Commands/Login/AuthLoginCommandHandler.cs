using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public sealed class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, ResponseDto<AuthLoginDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;
    private readonly IApplicationDbContext _context;

    public AuthLoginCommandHandler(IIdentityService identityService, IJwtService jwtService, IApplicationDbContext context)
    {
        _identityService = identityService;
        _jwtService = jwtService;
        _context = context;
    }

    public async Task<ResponseDto<AuthLoginDto>> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.LoginAsync(request.ToIdentityLoginRequest(), cancellationToken);

        return new ResponseDto<AuthLoginDto>(AuthLoginDto.FromIdentityLoginResponse(response));
    }
}