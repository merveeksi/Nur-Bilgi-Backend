using Microsoft.AspNetCore.Identity;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Features.Auth.Commands.Register;
using NurBilgi.Domain.Identity;

namespace NurBilgi.Infrastructure.Services;

public sealed class IdentityManager: IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityManager(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> RegisterAsync(AuthRegisterCommand command, CancellationToken cancellationToken)
    {
        var user = ApplicationUser.Create(command.FullName, command.Email);

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            throw new Exception(result.Errors.First().Description);
        }

        return user.Id.ToString();
    }
}