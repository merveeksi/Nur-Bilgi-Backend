using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.PrayerTimes.Queries.GetAll
{
    public class GetAllPrayerTimesQueryHandler : IRequestHandler<GetAllPrayerTimesQuery, List<PrayerTimeGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllPrayerTimesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PrayerTimeGetAllDto>> Handle(GetAllPrayerTimesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.PrayerTimes.AsQueryable();

            if (!string.IsNullOrEmpty(request.City))
                query = query.Where(x => x.City.ToLower() == request.City.ToLower());

            // Tarih filtresi: Saat bilgisi olmadan gün bazında filtreleme
            query = query.Where(x => x.Date.Date == request.Date.Date);

            return await query.AsNoTracking()
                .Select(x => new PrayerTimeGetAllDto(
                    x.Id, 
                    x.City, 
                    x.Date, 
                    x.Fajr, 
                    x.Dhuhr, 
                    x.Asr, 
                    x.Maghrib, 
                    x.Isha, 
                    x.Imsak))
                .ToListAsync(cancellationToken);
        }
    }
}