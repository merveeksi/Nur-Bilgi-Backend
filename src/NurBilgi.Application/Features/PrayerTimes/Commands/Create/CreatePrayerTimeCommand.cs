using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Create;

public record CreatePrayerTimeCommand: IRequest<ResponseDto<long>>
{
    public string City { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public TimeSpan Fajr { get; set; }
    public TimeSpan Dhuhr { get; set; }
    public TimeSpan Asr { get; set; }
    public TimeSpan Maghrib { get; set; }
    public TimeSpan Isha { get; set; }
    public TimeSpan Imsak { get; set; }

    public CreatePrayerTimeCommand(string city, DateTimeOffset date, TimeSpan fajr, TimeSpan dhuhr, TimeSpan asr, TimeSpan maghrib, TimeSpan isha, TimeSpan imsak)
    {
        City = city;
        Date = date;
        Fajr = fajr;
        Dhuhr = dhuhr;
        Asr = asr;
        Maghrib = maghrib;
        Isha = isha;
        Imsak = imsak;
    }

    public CreatePrayerTimeCommand()
    {
    }
}