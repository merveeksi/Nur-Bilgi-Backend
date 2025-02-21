using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Customers.Commands.Update;

public record UpdateCustomerCommand : IRequest
{
    public long Id { get; init; }
    public string UserName { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
        }

        entity.UserName = new UserName(request.UserName);
        entity.FullName = new FullName(request.FirstName, request.LastName);
        entity.Email = new Email(request.Email);
        entity.ModifiedOn = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
} 