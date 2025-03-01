namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetById;

public sealed record AiChatMessageGetByIdDto
{
    
    public long Id { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public bool IsCustomerMessage { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public long CustomerId { get; set; }

    public static AiChatMessageGetByIdDto Create(long id, string messageText, bool isCustomerMessage, DateTimeOffset timestamp, long customerId)
    {
        return new AiChatMessageGetByIdDto
        {
            Id = id,
            MessageText = messageText,
            IsCustomerMessage = isCustomerMessage,
            Timestamp = timestamp,
            CustomerId = customerId
        };
    }
    
    public AiChatMessageGetByIdDto(long id, string messageText, bool isCustomerMessage, DateTimeOffset timestamp, long customerId)
    {
        Id = id;
        MessageText = messageText;
        IsCustomerMessage = isCustomerMessage;
        Timestamp = timestamp;
        CustomerId = customerId;
    }
        public AiChatMessageGetByIdDto()
    {
        
    }
    
}