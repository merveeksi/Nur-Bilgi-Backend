using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Favorites.Queries.GetById
{
    public sealed class FavoriteGetByIdQueryValidator : AbstractValidator<FavoriteGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;

        public FavoriteGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(CheckIfFavoriteExists)
                .WithMessage("Favorite not found");
        }

        private Task<bool> CheckIfFavoriteExists(long id, CancellationToken cancellationToken)
        {
            return _context.Favorites
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}