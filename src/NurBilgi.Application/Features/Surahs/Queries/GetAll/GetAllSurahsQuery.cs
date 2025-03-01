using MediatR;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Surahs.Queries.GetAll
{
    public sealed record GetAllSurahsQuery : IRequest<List<SurahGetAllDto>>, ICacheable
    {
        public string Name { get; set; } = string.Empty;
        
        public string CacheGroup => "Surahs";

        public GetAllSurahsQuery(string name)
        {
            Name = name;
        }
    }
}