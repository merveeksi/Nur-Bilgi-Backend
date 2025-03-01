using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.PrayerTimes.Queries.GetById
{
    public sealed class PrayerTimeGetByIdQueryValidator : AbstractValidator<PrayerTimeGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;

        public PrayerTimeGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(CheckIfPrayerTimeExists)
                .WithMessage("PrayerTime not found");
        }

        private Task<bool> CheckIfPrayerTimeExists(long id, CancellationToken cancellationToken)
        {
            return _context.PrayerTimes
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}