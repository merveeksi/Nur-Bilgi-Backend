using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.Customers.Commands.Delete;

public sealed class DeleteCursomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;

    public DeleteCursomerCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.CustomerIdIsRequired])
            .MustAsync(CustomerExists)
            .WithMessage(_localizer[ValidationTranslationKeys.CustomerNotFound]);
    }

    private Task<bool> CustomerExists(long id, CancellationToken cancellationToken)
    {
        return _context.Customers
        .AsNoTracking()
        .AnyAsync(x => x.Id == id, cancellationToken);
    }
}