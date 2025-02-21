using System;
using NurBilgi.Domain.Common.Events;

namespace NurBilgi.Domain.DomainEvents;

public record NoteCreatedDomainEvent(long noteId) : IDomainEvent;

