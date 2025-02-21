using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Delete;

public sealed class DeleteDailyDuaCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeleteDailyDuaCommand(long id)
    {
        Id = id;
    }
} 