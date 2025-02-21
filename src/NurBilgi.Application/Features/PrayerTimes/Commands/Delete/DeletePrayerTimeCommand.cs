using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Delete;

public sealed class DeletePrayerTimeCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeletePrayerTimeCommand(long id)
    {
        Id = id;
    }
} 