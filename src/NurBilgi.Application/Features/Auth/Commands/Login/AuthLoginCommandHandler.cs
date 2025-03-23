using MediatR;
using Microsoft.AspNetCore.Identity;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.DomainEvents;
using NurBilgi.Domain.Identity;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public sealed class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, ResponseDto<AuthLoginDto>>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;
    private readonly IApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthLoginCommandHandler(
        IIdentityService identityService, 
        IJwtService jwtService, 
        IApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _identityService = identityService;
        _jwtService = jwtService;
        _context = context;
        _userManager = userManager;
    }

    public async Task<ResponseDto<AuthLoginDto>> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
    {
        // Login işlemi
        var response = await _identityService.LoginAsync(request.ToIdentityLoginRequest(), cancellationToken);
        
        // Kullanıcı bilgilerini al
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        // UserDto oluştur
        var userDto = new UserDto(
            user.Id,
            user.Email,
            user.FullName.FirstName,
            user.FullName.LastName
        );
        
        // AuthLoginDto oluştur
        var loginDto = new AuthLoginDto(
            response.Token,
            response.ExpiresAt,
            userDto
        );

        return new ResponseDto<AuthLoginDto>(loginDto);
    }
}