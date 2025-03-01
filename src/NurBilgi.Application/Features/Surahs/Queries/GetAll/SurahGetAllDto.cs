namespace NurBilgi.Application.Features.Surahs.Queries.GetAll
{
    public sealed record SurahGetAllDto
    {
        public long Id { get; set; }
        public int SurahNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AyahCount { get; set; }
        public string? ArabicText { get; set; }
        public string? Translation { get; set; }

        public SurahGetAllDto(long id, int surahNumber, string name, int ayahCount, string? arabicText, string? translation)
        {
            Id = id;
            SurahNumber = surahNumber;
            Name = name;
            AyahCount = ayahCount;
            ArabicText = arabicText;
            Translation = translation;
        }

        public SurahGetAllDto() { }

        public static SurahGetAllDto Create(long id, int surahNumber, string name, int ayahCount, string? arabicText, string? translation)
            => new SurahGetAllDto(id, surahNumber, name, ayahCount, arabicText, translation);
    }
}