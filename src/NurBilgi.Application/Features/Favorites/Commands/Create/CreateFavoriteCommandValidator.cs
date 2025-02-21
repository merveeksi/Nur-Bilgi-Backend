using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Favorites.Commands.Create;

public sealed class CreateFavoriteCommandValidator : AbstractValidator<CreateFavoriteCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateFavoriteCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.ContentId)
            .NotEmpty()
            .WithMessage("ContentId is required")
            .Must(x => _context.PrayerTimes.Any(p => p.Id == x))
            .WithMessage("Prayer time not found");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required")
            .Must(x => _context.Customers.Any(c => c.Id == x))
            .WithMessage("Customer not found");

        RuleFor(x => x.ContentType)
            .NotEmpty()
            .WithMessage("ContentType is required")
            .IsInEnum()
            .WithMessage("Invalid content type");
    }
}