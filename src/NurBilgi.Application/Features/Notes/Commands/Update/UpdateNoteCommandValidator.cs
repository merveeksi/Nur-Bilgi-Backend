using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Notes.Commands.Update;

public sealed class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateNoteCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) => 
                await _context.Notes.AnyAsync(n => n.Id == id && !n.IsDeleted, ct))
            .WithMessage("Note not found");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content is required");
    }
}