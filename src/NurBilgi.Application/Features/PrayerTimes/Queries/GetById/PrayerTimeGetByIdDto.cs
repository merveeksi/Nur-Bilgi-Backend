namespace NurBilgi.Application.Features.PrayerTimes.Queries.GetById
{
    public sealed record PrayerTimeGetByIdDto
    {
        public long Id { get; set; }
        public string City { get; set; } = string.Empty;
        public DateTimeOffset Date { get; set; }
        public TimeSpan Fajr { get; set; }
        public TimeSpan Dhuhr { get; set; }
        public TimeSpan Asr { get; set; }
        public TimeSpan Maghrib { get; set; }
        public TimeSpan Isha { get; set; }
        public TimeSpan Imsak { get; set; }

        public static PrayerTimeGetByIdDto Create(long id, string city, DateTimeOffset date, TimeSpan fajr, TimeSpan dhuhr, TimeSpan asr, TimeSpan maghrib, TimeSpan isha, TimeSpan imsak)
        {
            return new PrayerTimeGetByIdDto
            {
                Id = id,
                City = city,
                Date = date,
                Fajr = fajr,
                Dhuhr = dhuhr,
                Asr = asr,
                Maghrib = maghrib,
                Isha = isha,
                Imsak = imsak
            };
        }

        public PrayerTimeGetByIdDto(long id, string city, DateTimeOffset date, TimeSpan fajr, TimeSpan dhuhr, TimeSpan asr, TimeSpan maghrib, TimeSpan isha, TimeSpan imsak)
        {
            Id = id;
            City = city;
            Date = date;
            Fajr = fajr;
            Dhuhr = dhuhr;
            Asr = asr;
            Maghrib = maghrib;
            Isha = isha;
            Imsak = imsak;
        }

        public PrayerTimeGetByIdDto() { }
    }
}