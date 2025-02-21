using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Create;

public record CreateAiChatMessageCommand : IRequest<ResponseDto<long>>
{
    public string MessageText { get; set; }
    public bool IsCustomerMessage { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public long CustomerId { get; set; }

    public CreateAiChatMessageCommand(string messageText, bool isCustomerMessage, DateTimeOffset timestamp, long customerId)
    {
        MessageText = messageText;
        IsCustomerMessage = isCustomerMessage;
        Timestamp = timestamp;
        CustomerId = customerId;
    }

    public CreateAiChatMessageCommand()
    {
    }

}
