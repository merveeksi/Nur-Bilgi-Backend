using NurBilgi.Application.Features.Auth.Commands.Register;

namespace NurBilgi.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> RegisterAsync(AuthRegisterCommand command, CancellationToken cancellationToken);
}