using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateCustomerCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            UserName = request.UserName,
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = request.PasswordHash
        };

        _context.Customers.Add(customer);

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Customers", cancellationToken);

        return ResponseDto<long>.Success(customer.Id, "Customer created successfully.");
    }
}   