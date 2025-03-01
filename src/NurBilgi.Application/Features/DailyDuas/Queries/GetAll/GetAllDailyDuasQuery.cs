using MediatR;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.DailyDuas.Queries.GetAll
{
    public sealed record GetAllDailyDuasQuery : IRequest<List<DailyDuaGetAllDto>>, ICacheable
    {
        public string DuaText { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? TimeOfDay { get; set; }

        public string CacheGroup => "DailyDuas";

        public GetAllDailyDuasQuery(string duaText, string? category, string? timeOfDay)
        {
            DuaText = duaText;
            Category = category;
            TimeOfDay = timeOfDay;
        }
    }
}