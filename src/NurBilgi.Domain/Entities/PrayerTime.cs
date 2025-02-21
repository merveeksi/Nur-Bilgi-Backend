using NurBilgi.Domain.Common.Entities;

namespace NurBilgi.Domain.Entities;

public sealed class PrayerTime
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
}
