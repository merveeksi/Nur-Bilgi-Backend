using MediatR;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.PrayerTimes.Queries.GetAll
{
    public sealed record GetAllPrayerTimesQuery : IRequest<List<PrayerTimeGetAllDto>>, ICacheable
    {
        public string City { get; set; } = string.Empty;
        public DateTimeOffset Date { get; set; }
        
        public string CacheGroup => "PrayerTimes";

        public GetAllPrayerTimesQuery(string city, DateTimeOffset date)
        {
            City = city;
            Date = date;
        }
    }
}