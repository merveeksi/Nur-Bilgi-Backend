using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.PrayerTimes.Queries.GetById
{
    public sealed class PrayerTimeGetByIdQueryHandler : IRequestHandler<PrayerTimeGetByIdQuery, PrayerTimeGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public PrayerTimeGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PrayerTimeGetByIdDto> Handle(PrayerTimeGetByIdQuery request, CancellationToken cancellationToken)
        {
            var prayerTimeDto = await _context.PrayerTimes
                .AsNoTracking()
                .Select(x => new PrayerTimeGetByIdDto(x.Id, x.City, x.Date, x.Fajr, x.Dhuhr, x.Asr, x.Maghrib, x.Isha, x.Imsak))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return prayerTimeDto!;
        }
    }
}