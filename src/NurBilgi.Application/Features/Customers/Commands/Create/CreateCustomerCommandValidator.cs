using System;
using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Customers.Commands.Create;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.UserName.Value)
            .NotEmpty()
            .WithMessage("UserName is required")
            .Must(x => !_context.Customers.Any(c => c.UserName.Value == x))
            .WithMessage("UserName already exists");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("FullName is required");

        RuleFor(x => x.FullName.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required")
            .MinimumLength(2)
            .WithMessage("FirstName must be at least 2 characters")
            .MaximumLength(50)
            .WithMessage("FirstName must be less than 50 characters");

        RuleFor(x => x.FullName.LastName)
            .NotEmpty()
            .WithMessage("LastName is required")
            .MinimumLength(2)
            .WithMessage("LastName must be at least 2 characters")
            .MaximumLength(50)
            .WithMessage("LastName must be less than 50 characters");

        RuleFor(x => x.Email.Value)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .Must(x => !_context.Customers.Any(c => c.Email.Value == x))
            .WithMessage("Email already exists");

        RuleFor(x => x.PasswordHash.Value)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters");
    }
}