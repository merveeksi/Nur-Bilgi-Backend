using System;
using System.Collections.Generic;

namespace NurBilgi.Domain.Entities;

public sealed class Surah
{
    public long Id { get; set; }
    public int SurahNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AyahCount { get; set; }
    public string? ArabicText { get; set; }
    public string? Translation { get; set; }
} 