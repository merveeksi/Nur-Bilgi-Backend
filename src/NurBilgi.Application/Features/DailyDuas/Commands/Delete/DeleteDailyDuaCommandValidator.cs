using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Delete;

public sealed class DeleteDailyDuaCommandValidator : AbstractValidator<DeleteDailyDuaCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;

    public DeleteDailyDuaCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.DailyDuaDeleteIdRequired])
            .MustAsync(DailyDuaExist)
            .WithMessage(_localizer[ValidationTranslationKeys.DailyDuaDeleteIdNotFound]);
    }

    private Task<bool> DailyDuaExist(long id, CancellationToken cancellationToken)
    {
        return _context.DailyDuas
            .AsNoTracking()
            .AnyAsync(d => d.Id == id, cancellationToken);
    }
} 