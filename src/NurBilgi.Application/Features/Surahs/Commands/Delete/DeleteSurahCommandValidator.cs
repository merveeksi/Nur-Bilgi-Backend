using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.Surahs.Commands.Delete;

public sealed class DeleteSurahCommandValidator : AbstractValidator<DeleteSurahCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;

    public DeleteSurahCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.SurahDeleteIdRequired])
            .MustAsync(SurahExist)
            .WithMessage(_localizer[ValidationTranslationKeys.SurahDeleteIdNotFound]);
    }

    private Task<bool> SurahExist(long id, CancellationToken cancellationToken)
    {
        return _context.Surahs
            .AsNoTracking()
            .AnyAsync(s => s.Id == id, cancellationToken);
    }
} 