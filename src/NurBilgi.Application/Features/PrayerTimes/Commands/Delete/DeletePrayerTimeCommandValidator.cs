using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Delete;

public sealed class DeletePrayerTimeCommandValidator : AbstractValidator<DeletePrayerTimeCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;

    public DeletePrayerTimeCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.PrayerTimeDeleteIdRequired])
            .MustAsync(PrayerTimeExist)
            .WithMessage(_localizer[ValidationTranslationKeys.PrayerTimeDeleteIdNotFound]);
    }

    private Task<bool> PrayerTimeExist(long id, CancellationToken cancellationToken)
    {
        return _context.PrayerTimes
            .AsNoTracking()
            .AnyAsync(p => p.Id == id, cancellationToken);
    }
} 