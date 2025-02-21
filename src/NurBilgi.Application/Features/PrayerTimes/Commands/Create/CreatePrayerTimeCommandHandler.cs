using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Create;

public sealed class CreatePrayerTimeCommandHandler: IRequestHandler<CreatePrayerTimeCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreatePrayerTimeCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreatePrayerTimeCommand request, CancellationToken cancellationToken)
    {
        var prayerTime = new PrayerTime
        {
            City = request.City,
            Date = request.Date,
            Fajr = request.Fajr,
            Dhuhr = request.Dhuhr,
            Asr = request.Asr,
            Maghrib = request.Maghrib,
            Isha = request.Isha,
            Imsak = request.Imsak
        };

        _context.PrayerTimes.Add(prayerTime);
        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("PrayerTimes", cancellationToken);

        return ResponseDto<long>.Success(prayerTime.Id, "Prayer time created successfully.");
    }
}
