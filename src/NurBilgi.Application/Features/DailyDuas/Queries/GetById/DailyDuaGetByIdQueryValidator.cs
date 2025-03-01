using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.DailyDuas.Queries.GetById
{
    public sealed class DailyDuaGetByIdQueryValidator : AbstractValidator<DailyDuaGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;

        public DailyDuaGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(CheckIfDailyDuaExists)
                .WithMessage("DailyDua not found");
        }

        private Task<bool> CheckIfDailyDuaExists(long id, CancellationToken cancellationToken)
        {
            return _context.DailyDuas
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}