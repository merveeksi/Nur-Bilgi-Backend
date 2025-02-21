using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Update;

public sealed class UpdatePrayerTimeCommandHandler 
    : IRequestHandler<UpdatePrayerTimeCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdatePrayerTimeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdatePrayerTimeCommand request, CancellationToken cancellationToken)
    {
        var prayerTime = await _context.PrayerTimes
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (prayerTime is null)
        {
            return ResponseDto<long>.Error("PrayerTime not found");
        }

        prayerTime.City = request.City;
        prayerTime.Date = request.Date;
        prayerTime.Fajr = request.Fajr;
        prayerTime.Dhuhr = request.Dhuhr;
        prayerTime.Asr = request.Asr;
        prayerTime.Maghrib = request.Maghrib;
        prayerTime.Isha = request.Isha;
        prayerTime.Imsak = request.Imsak;

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(prayerTime.Id, "PrayerTime updated successfully");
    }
}