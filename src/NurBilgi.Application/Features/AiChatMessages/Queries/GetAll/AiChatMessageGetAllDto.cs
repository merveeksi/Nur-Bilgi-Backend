using System;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetAll;

public sealed record AiChatMessageGetAllDto
{
    public long Id { get; set; }
    public string MessageText { get; set; } = string.Empty;
    public bool IsCustomerMessage { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public static AiChatMessageGetAllDto Create(long id, string messageText, bool isCustomerMessage, DateTimeOffset timestamp)
    {
        return new AiChatMessageGetAllDto
        {
            Id = id,
            MessageText = messageText,
            IsCustomerMessage = isCustomerMessage,
            Timestamp = timestamp
        };
    }
    public AiChatMessageGetAllDto(long id, string messageText, bool isCustomerMessage, DateTimeOffset timestamp)
    {
        Id = id;
        MessageText = messageText;
        IsCustomerMessage = isCustomerMessage;
        Timestamp = timestamp;
    }

    public AiChatMessageGetAllDto()
    {
    }
}
