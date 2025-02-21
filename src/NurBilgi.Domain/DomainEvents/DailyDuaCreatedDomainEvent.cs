using NurBilgi.Domain.Common.Events;

namespace NurBilgi.Domain.DomainEvents;

public record DailyDuaCreatedDomainEvent(long dailyDuaId) : IDomainEvent;
