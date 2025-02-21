using MediatR;
using NurBilgi.Application.Common.Models.Responses;


namespace NurBilgi.Application.Features.AiChatMessages.Commands.Update;

public sealed record UpdateAiChatMessageCommand(
    long Id, 
    string MessageText, 
    bool IsCustomerMessage,
    DateTimeOffset Timestamp,
    long CustomerId
    ) : IRequest<ResponseDto<long>>;
