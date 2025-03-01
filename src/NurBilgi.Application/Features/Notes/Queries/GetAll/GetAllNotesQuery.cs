using MediatR;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Notes.Queries.GetAll
{
    public sealed record GetAllNotesQuery : IRequest<List<NoteGetAllDto>>, ICacheable
    {
        [CacheKeyPart]
        public long CustomerId { get; set; }
        public string Title { get; set; } = string.Empty;

        public string CacheGroup => "Notes";

        public GetAllNotesQuery(long customerId, string title)
        {
            CustomerId = customerId;
            Title = title;
        }
    }
}