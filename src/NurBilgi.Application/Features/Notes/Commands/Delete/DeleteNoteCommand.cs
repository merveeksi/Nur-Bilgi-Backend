using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Notes.Commands.Delete;

public sealed class DeleteNoteCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeleteNoteCommand(long id)
    {
        Id = id;
    }
} 