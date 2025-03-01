namespace NurBilgi.Application.Features.Surahs.Queries.GetById
{
    public sealed record SurahGetByIdDto
    {
        public long Id { get; set; }
        public int SurahNumber { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AyahCount { get; set; }
        public string? ArabicText { get; set; }
        public string? Translation { get; set; }

        public static SurahGetByIdDto Create(long id, int surahNumber, string name, int ayahCount, string? arabicText, string? translation)
        {
            return new SurahGetByIdDto
            {
                Id = id,
                SurahNumber = surahNumber,
                Name = name,
                AyahCount = ayahCount,
                ArabicText = arabicText,
                Translation = translation
            };
        }

        public SurahGetByIdDto(long id, int surahNumber, string name, int ayahCount, string? arabicText, string? translation)
        {
            Id = id;
            SurahNumber = surahNumber;
            Name = name;
            AyahCount = ayahCount;
            ArabicText = arabicText;
            Translation = translation;
        }

        public SurahGetByIdDto() { }
    }
}