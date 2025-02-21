using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.Favorites.Commands.Delete;

public sealed class DeleteFavoriteCommandValidator : AbstractValidator<DeleteFavoriteCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;

    public DeleteFavoriteCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.FavoriteDeleteIdRequired])
            .MustAsync(FavoriteExist)
            .WithMessage(_localizer[ValidationTranslationKeys.FavoriteDeleteIdNotFound]);
    }

    private Task<bool> FavoriteExist(long id, CancellationToken cancellationToken)
    {
        return _context.Favorites
            .AsNoTracking()
            .AnyAsync(f => f.Id == id, cancellationToken);
    }
} 