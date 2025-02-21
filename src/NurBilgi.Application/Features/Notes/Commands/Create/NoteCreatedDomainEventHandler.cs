using System;
using MediatR;
using Microsoft.Extensions.Logging;
using NurBilgi.Domain.DomainEvents;

namespace NurBilgi.Application.Features.Notes.Commands.Create;

public sealed class NoteCreatedDomainEventHandler: INotificationHandler<NoteCreatedDomainEvent>
{
    private readonly ILogger<NoteCreatedDomainEventHandler> _logger;

    public NoteCreatedDomainEventHandler(ILogger<NoteCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(NoteCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Note created: {NoteId}", notification.noteId);

        return Task.CompletedTask;
    }
}
