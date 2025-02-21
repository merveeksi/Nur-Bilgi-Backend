using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.DomainEvents;
using TSID.Creator.NET;

namespace NurBilgi.Domain.Entities;

public sealed class AiChatMessage : EntityBase<long>
{
    public string MessageText { get; set; } = string.Empty;
    public bool IsCustomerMessage { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    
    // Foreign keys
    public long CustomerId { get; set; }
    
    // Navigation properties
    public Customer Customer { get; set; }

    public static AiChatMessage Create(string messageText, bool isCustomerMessage, DateTimeOffset timestamp, long customerId)
    {
        var aiChatMessage = new AiChatMessage
        {
            Id = TsidCreator.GetTsid().ToLong(),
            MessageText = messageText,
            IsCustomerMessage = isCustomerMessage,
            Timestamp = timestamp,
            CustomerId = customerId
        };
        
        aiChatMessage.RaiseDomainEvent(new AiChatMessageCreatedDomainEvent(aiChatMessage.Id));

        return aiChatMessage;
    }
    
} 