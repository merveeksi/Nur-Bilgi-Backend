using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Commands.Update;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) =>
                await _context.Customers.AnyAsync(c => c.Id == id && !c.IsDeleted, ct))
            .WithMessage("Customer not found");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required")
            .MaximumLength(50).WithMessage("UserName cannot exceed 50 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}