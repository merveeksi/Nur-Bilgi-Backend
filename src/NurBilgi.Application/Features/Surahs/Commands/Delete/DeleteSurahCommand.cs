using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Surahs.Commands.Delete;

public sealed class DeleteSurahCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeleteSurahCommand(long id)
    {
        Id = id;
    }
} 