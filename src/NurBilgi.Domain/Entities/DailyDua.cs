using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.DomainEvents;
using TSID.Creator.NET;

namespace NurBilgi.Domain.Entities;

public sealed class DailyDua : EntityBase<long>
{
    public long Id { get; set; }
    public string DuaText { get; set; } = string.Empty;
    public string? ArabicText { get; set; }
    public string? Category { get; set; }
    public string? Source { get; set; } // kaynak
    public string? TimeOfDay { get; set; }

    public static DailyDua Create(string duaText, string? arabicText, string? category, string? source, string? timeOfDay)
    {
        var dailyDua = new DailyDua
        {
            Id = TsidCreator.GetTsid().ToLong(),
            DuaText = duaText,
            ArabicText = arabicText,
            Category = category,
            Source = source,
            TimeOfDay = timeOfDay
        };

        dailyDua.RaiseDomainEvent(new DailyDuaCreatedDomainEvent(dailyDua.Id));

        return dailyDua;
    }
}
