using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Queries.GetById
{
    public sealed class CustomerGetByIdQueryValidator : AbstractValidator<CustomerGetByIdQuery>
    {
        private readonly IApplicationDbContext _context;

        public CustomerGetByIdQueryValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(CheckIfCustomerExists)
                .WithMessage("Customer not found");
        }

        private Task<bool> CheckIfCustomerExists(long id, CancellationToken cancellationToken)
        {
            return _context.Customers
                .AsNoTracking()
                .AnyAsync(x => x.Id == id, cancellationToken);
        }
    }
}