using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.Customers.Commands.Delete;

public record DeleteCustomerCommand(long Id) : IRequest;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
        }

        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
} 