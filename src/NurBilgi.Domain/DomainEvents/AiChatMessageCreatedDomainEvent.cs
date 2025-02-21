using NurBilgi.Domain.Common.Events;

namespace NurBilgi.Domain.DomainEvents;

public record AiChatMessageCreatedDomainEvent(long aiChatMessageId) : IDomainEvent;