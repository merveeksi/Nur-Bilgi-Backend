using FluentValidation;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Auth.Commands.Register;

public sealed class AuthRegisterCommandValidator: AbstractValidator<AuthRegisterCommand>
{
    private readonly IIdentityService _identityService;
    public AuthRegisterCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);

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
            .WithMessage("LastName must be at least 2 characters");


        RuleFor(x => x.Email)
            .MustAsync(CheckEmailExistsAsync)
            .WithMessage("The email is already in use.");
    }

    private async Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return !await _identityService.CheckEmailExistsAsync(email, cancellationToken);
    }
}