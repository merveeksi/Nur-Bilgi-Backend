using NurBilgi.Application.Features.Auth.Commands.Register;

namespace NurBilgi.Application.Common.Interfaces;

public interface IMessagePublisher
{
    Task PublishUserRegisteredMessageAsync(
        UserRegisteredMessage message,
        CancellationToken cancellationToken = default
    );
}