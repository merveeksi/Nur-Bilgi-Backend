using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Update;

public sealed record UpdatePrayerTimeCommand(
    long Id,
    string City,
    DateTimeOffset Date,
    TimeSpan Fajr,
    TimeSpan Dhuhr,
    TimeSpan Asr,
    TimeSpan Maghrib,
    TimeSpan Isha,
    TimeSpan Imsak
) : IRequest<ResponseDto<long>>;