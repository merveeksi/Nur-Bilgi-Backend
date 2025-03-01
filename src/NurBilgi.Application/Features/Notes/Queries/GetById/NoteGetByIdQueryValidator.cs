using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Notes.Queries.GetById
{
    public sealed class NoteGetByIdQueryValidator : AbstractValidator<NoteGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;

        public NoteGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(CheckIfNoteExists)
                .WithMessage("Note not found");
        }

        private Task<bool> CheckIfNoteExists(long id, CancellationToken cancellationToken)
        {
            return _context.Notes
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}