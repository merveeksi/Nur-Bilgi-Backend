using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Notes.Queries.GetById
{
    public sealed class NoteGetByIdQueryHandler : IRequestHandler<NoteGetByIdQuery, NoteGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public NoteGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NoteGetByIdDto> Handle(NoteGetByIdQuery request, CancellationToken cancellationToken)
        {
            var noteDto = await _context.Notes
                .AsNoTracking()
                .Select(x => new NoteGetByIdDto(x.Id, x.Title, x.Content, x.CustomerId))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return noteDto!;
        }
    }
}