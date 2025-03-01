using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Queries.GetById
{
    public sealed class CustomerGetByIdQueryHandler : IRequestHandler<CustomerGetByIdQuery, CustomerGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public CustomerGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerGetByIdDto> Handle(CustomerGetByIdQuery request, CancellationToken cancellationToken)
        {
            var customerDto = await _context.Customers
                .AsNoTracking()
                .Select(x => new CustomerGetByIdDto(
                    x.Id,
                    x.UserName.Value,
                    x.FullName.ToString(),
                    x.Email.Value))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return customerDto!;
        }
    }
}