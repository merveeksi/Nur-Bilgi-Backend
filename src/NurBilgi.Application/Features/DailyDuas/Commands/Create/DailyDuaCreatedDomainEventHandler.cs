using System;
using MediatR;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Create;

public sealed class DailyDuaCreatedDomainEventHandler : INotificationHandler<DailyDuaCreatedDomainEvent>
{
    private readonly ILogger<DailyDuaCreatedDomainEventHandler> _logger;

    public DailyDuaCreatedDomainEventHandler(ILogger<DailyDuaCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(DailyDuaCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Daily dua created: {DailyDuaId}", notification.dailyDuaId);

        return Task.CompletedTask;
    }
}
