using MediatR;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Catechisms.Queries.GetAll
{
    public sealed record GetAllCatechismsQuery : IRequest<List<CatechismGetAllDto>>, ICacheable
    {
        public string BookName { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        
        public string CacheGroup => "Catechisms";

        public GetAllCatechismsQuery(string bookName, string authorName, string tags)
        {
            BookName = bookName;
            AuthorName = authorName;
            Tags = tags;
        }
    }
} 