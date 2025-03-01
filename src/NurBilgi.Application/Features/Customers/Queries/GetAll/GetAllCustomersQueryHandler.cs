using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCustomersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerGetAllDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                // UserName, FullName ve Email value object’lerinin string temsiline bağlı olarak filtreleme yapılabilir.
                query = query.Where(x =>
                    x.UserName.Value.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    x.FullName.ToString().ToLower().Contains(request.SearchTerm.ToLower()) ||
                    x.Email.Value.ToLower().Contains(request.SearchTerm.ToLower()));
            }

            return await query.AsNoTracking()
                .Select(x => new CustomerGetAllDto(x.Id, x.UserName.Value, x.FullName.ToString(), x.Email.Value))
                .ToListAsync(cancellationToken);
        }
    }
}