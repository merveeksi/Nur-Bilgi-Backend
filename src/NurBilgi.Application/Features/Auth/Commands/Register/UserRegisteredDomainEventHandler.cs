using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.Auth.Commands.Register;


public sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    
    public UserRegisteredDomainEventHandler()
    {
        
    }
    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {

        // TODO: Generate verification token

        var message = new UserRegisteredMessage(notification.Id, notification.Email, notification.FullName, "123456");
    }
}