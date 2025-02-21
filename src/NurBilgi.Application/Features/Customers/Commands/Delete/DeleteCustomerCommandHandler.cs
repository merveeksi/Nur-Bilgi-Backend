using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Customers.Commands.Delete;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ResponseDto<long>>

{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        _context.Customers.Remove(customer);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(customer.Id, "Customer deleted successfully", true);
    }
} 