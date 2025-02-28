using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetAll;

public sealed record GetAllAiChatMessagesQuery : IRequest<List<AiChatMessageGetAllDto>>, ICacheable
{
     public string MessageText { get; set; } = string.Empty;
     public bool IsCustomerMessage { get; set; }
     public DateTimeOffset Timestamp { get; set; }
     [CacheKeyPart]
     public long CustomerId { get; set; }

     public string CacheGroup => "AiChatMessages";

     public GetAllAiChatMessagesQuery(string messageText, bool isCustomerMessage, DateTimeOffset timestamp, long customerId)
     {
        MessageText = messageText;
        IsCustomerMessage = isCustomerMessage;
        Timestamp = timestamp;
        CustomerId = customerId;
     }

};
