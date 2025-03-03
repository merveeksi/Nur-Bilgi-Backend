using NurBilgi.Application.Common.Models.Auth;
using NurBilgi.Application.Common.Models.Identity;
using NurBilgi.Application.Features.Auth.Commands.Register;

namespace NurBilgi.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> RegisterAsync(AuthRegisterCommand command, CancellationToken cancellationToken);
    Task<bool> AuthenticateAsync(IdentityAuthenticateRequest request, CancellationToken cancellationToken);

    Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken);

    Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken);

    Task<bool> CheckSecurityStampAsync(Guid userId, string securityStamp, CancellationToken cancellationToken);

    Task<IdentityRegisterResponse> RegisterAsync(IdentityRegisterRequest request, CancellationToken cancellationToken);
    Task<IdentityLoginResponse> LoginAsync(IdentityLoginRequest request, CancellationToken cancellationToken);
    Task<IdentityVerifyEmailResponse> VerifyEmailAsync(IdentityVerifyEmailRequest request, CancellationToken cancellationToken);

    Task<IdentityCreateEmailTokenResponse> CreateEmailTokenAsync(IdentityCreateEmailTokenRequest request, CancellationToken cancellationToken);
}