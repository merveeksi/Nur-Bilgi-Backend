using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.Notes.Commands.Delete;

public sealed class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;

    public DeleteNoteCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.NoteDeleteIdRequired])
            .MustAsync(NoteExist)
            .WithMessage(_localizer[ValidationTranslationKeys.NoteDeleteIdNotFound]);
    }

    private Task<bool> NoteExist(long id, CancellationToken cancellationToken)
    {
        return _context.Notes
            .AsNoTracking()
            .AnyAsync(n => n.Id == id, cancellationToken);
    }
} 