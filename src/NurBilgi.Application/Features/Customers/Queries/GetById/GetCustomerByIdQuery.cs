using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Features.Customers.Queries.GetAll;

namespace NurBilgi.Application.Features.Customers.Queries.GetById;

public record GetCustomerByIdQuery(long Id) : IRequest<CustomerDto>;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
        }

        return new CustomerDto
        {
            Id = entity.Id,
            UserName = entity.UserName.Value,
            FirstName = entity.FullName.FirstName,
            LastName = entity.FullName.LastName,
            Email = entity.Email.Value,
            CreatedAt = entity.CreatedOn,
            UpdatedAt = entity.ModifiedOn
        };
    }
} 