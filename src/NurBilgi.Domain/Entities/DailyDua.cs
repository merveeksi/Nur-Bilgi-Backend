using NurBilgi.Domain.Common.Entities;

namespace NurBilgi.Domain.Entities;

public sealed class DailyDua : EntityBase<long>
{
    public long Id { get; set; }
    public string DuaText { get; set; } = string.Empty;
    public string? ArabicText { get; set; }
    public string? Category { get; set; }
    public string? Source { get; set; }
    public string? TimeOfDay { get; set; }
} 