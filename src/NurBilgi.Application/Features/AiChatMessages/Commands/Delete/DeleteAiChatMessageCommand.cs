using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Delete;

public sealed class DeleteAiChatMessageCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeleteAiChatMessageCommand(long id)
    {
        Id = id;
    }
}