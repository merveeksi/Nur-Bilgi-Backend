using MediatR;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.Messages.Commands.Create;

public sealed class MessageCreatedDomainEventHandler : INotificationHandler<MessageCreatedDomainEvent>
{
    private readonly ILogger<MessageCreatedDomainEventHandler> _logger;

    public MessageCreatedDomainEventHandler(ILogger<MessageCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MessageCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message created: {messageId}", notification.MessageId);

        return Task.CompletedTask;
    }
} 