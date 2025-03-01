using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Surahs.Queries.GetById
{
    public sealed class SurahGetByIdQueryValidator : AbstractValidator<SurahGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;

        public SurahGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(CheckIfSurahExists)
                .WithMessage("Surah not found");
        }

        private Task<bool> CheckIfSurahExists(long id, CancellationToken cancellationToken)
        {
            return _context.Surahs
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}