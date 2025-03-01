using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Notes.Queries.GetAll
{
    public class GetAllNotesQueryHandler : IRequestHandler<GetAllNotesQuery, List<NoteGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllNotesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<NoteGetAllDto>> Handle(GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Notes.AsQueryable();

            query = query.Where(x => x.CustomerId == request.CustomerId);

            if (!string.IsNullOrEmpty(request.Title))
                query = query.Where(x => x.Title.ToLower().Contains(request.Title.ToLower()));

            return await query.AsNoTracking()
                .Select(x => new NoteGetAllDto(x.Id, x.Title, x.Content, x.CustomerId))
                .ToListAsync(cancellationToken);
        }
    }
}