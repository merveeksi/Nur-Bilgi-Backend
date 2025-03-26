using NurBilgi.Domain.Common.Events;

namespace NurBilgi.Domain.DomainEvents;

public record MessageCreatedDomainEvent(long MessageId) : IDomainEvent; 