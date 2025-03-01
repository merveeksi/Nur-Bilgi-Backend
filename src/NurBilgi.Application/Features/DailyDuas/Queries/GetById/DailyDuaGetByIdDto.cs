namespace NurBilgi.Application.Features.DailyDuas.Queries.GetById
{
    public sealed record DailyDuaGetByIdDto
    {
        public long Id { get; set; }
        public string DuaText { get; set; } = string.Empty;
        public string? ArabicText { get; set; }
        public string? Category { get; set; }
        public string? Source { get; set; }
        public string? TimeOfDay { get; set; }

        public static DailyDuaGetByIdDto Create(long id, string duaText, string? arabicText, string? category, string? source, string? timeOfDay)
        {
            return new DailyDuaGetByIdDto
            {
                Id = id,
                DuaText = duaText,
                ArabicText = arabicText,
                Category = category,
                Source = source,
                TimeOfDay = timeOfDay
            };
        }

        public DailyDuaGetByIdDto(long id, string duaText, string? arabicText, string? category, string? source, string? timeOfDay)
        {
            Id = id;
            DuaText = duaText;
            ArabicText = arabicText;
            Category = category;
            Source = source;
            TimeOfDay = timeOfDay;
        }

        public DailyDuaGetByIdDto() { }
    }
}