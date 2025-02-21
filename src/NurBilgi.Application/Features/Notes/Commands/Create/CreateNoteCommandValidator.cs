using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Notes.Commands.Create;

public sealed class CreateNoteCommandValidator: AbstractValidator<CreateNoteCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateNoteCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Note title cannot be empty.")
            .MaximumLength(200)
            .WithMessage("Note title cannot be longer than 200 characters.");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Note content cannot be empty.")
            .MaximumLength(4000)
            .WithMessage("Note content cannot be longer than 4000 characters.");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("UserId is required")
            .Must(x => _context.Customers.Any(c => c.Id == x))
            .WithMessage("User not found");
    }
}