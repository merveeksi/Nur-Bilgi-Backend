using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Queries.GetAll;

public record GetAllCustomersQuery : IRequest<List<CustomerDto>>;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Select(x => new CustomerDto
            {
                Id = x.Id,
                UserName = x.UserName.Value,
                FirstName = x.FullName.FirstName,
                LastName = x.FullName.LastName,
                Email = x.Email.Value,
                CreatedAt = x.CreatedOn,
                UpdatedAt = x.ModifiedOn
            })
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}

public class CustomerDto
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
} 