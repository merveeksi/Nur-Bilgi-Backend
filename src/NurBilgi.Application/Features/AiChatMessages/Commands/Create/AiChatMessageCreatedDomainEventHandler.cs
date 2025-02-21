using System;
using MediatR;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Create;

public sealed class AiChatMessageCreatedDomainEventHandler : INotificationHandler<AiChatMessageCreatedDomainEvent>
{
    private readonly ILogger<AiChatMessageCreatedDomainEventHandler> _logger;

    public AiChatMessageCreatedDomainEventHandler(ILogger<AiChatMessageCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(AiChatMessageCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ai chat message created: {aiChatMessageId}", notification.aiChatMessageId);

        return Task.CompletedTask;
    }
}
