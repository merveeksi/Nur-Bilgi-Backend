using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Customers.Commands.Update;

public sealed class UpdateCustomerCommandHandler 
    : IRequestHandler<UpdateCustomerCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

        if (customer is null)
        {
            return ResponseDto<long>.Error("Customer not found");
        }

        // Kullanıcının gerçekten kendi profilini mi güncellediğini kontrol etmek isterseniz:
        // if (authenticatedUserId != request.Id)
        //     return ResponseDto<long>.Error("You do not have permission to update this profile");

        // Value Object dönüşümleri
        customer.UserName = new UserName(request.UserName);
        customer.FullName = new FullName(request.FirstName, request.LastName);
        customer.Email = new Email(request.Email);
        customer.PasswordHash = new Password(request.Password);

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(customer.Id, "Customer updated successfully");
    }
}
