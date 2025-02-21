using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Favorites.Commands.Update;

public sealed class UpdateFavoriteCommandValidator : AbstractValidator<UpdateFavoriteCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateFavoriteCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) => 
                await _context.Favorites.AnyAsync(f => f.Id == id, ct))
            .WithMessage("Favorite not found");

        RuleFor(x => x.ContentType)
            .IsInEnum().WithMessage("Invalid FavoriteContentType value");

        RuleFor(x => x.ContentId)
            .GreaterThan(0).WithMessage("ContentId must be greater than 0");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId must be greater than 0");
    }
}