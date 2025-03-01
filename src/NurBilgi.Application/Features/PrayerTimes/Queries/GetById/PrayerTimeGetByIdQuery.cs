using MediatR;

namespace NurBilgi.Application.Features.PrayerTimes.Queries.GetById
{
    public sealed record PrayerTimeGetByIdQuery(long Id) : IRequest<PrayerTimeGetByIdDto>;
}