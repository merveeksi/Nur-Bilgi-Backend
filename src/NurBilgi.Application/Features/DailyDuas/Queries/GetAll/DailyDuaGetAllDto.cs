namespace NurBilgi.Application.Features.DailyDuas.Queries.GetAll
{
    public sealed record DailyDuaGetAllDto
    {
        public long Id { get; set; }
        public string DuaText { get; set; } = string.Empty;
        public string? ArabicText { get; set; }
        public string? Category { get; set; }
        public string? Source { get; set; }
        public string? TimeOfDay { get; set; }

        public DailyDuaGetAllDto(long id, string duaText, string? arabicText, string? category, string? source, string? timeOfDay)
        {
            Id = id;
            DuaText = duaText;
            ArabicText = arabicText;
            Category = category;
            Source = source;
            TimeOfDay = timeOfDay;
        }

        public DailyDuaGetAllDto() { }

        public static DailyDuaGetAllDto Create(long id, string duaText, string? arabicText, string? category, string? source, string? timeOfDay)
            => new DailyDuaGetAllDto(id, duaText, arabicText, category, source, timeOfDay);
    }
}