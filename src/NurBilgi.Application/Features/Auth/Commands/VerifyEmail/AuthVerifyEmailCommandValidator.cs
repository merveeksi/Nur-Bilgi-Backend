using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Auth.Commands.VerifyEmail;

public class AuthVerifyEmailCommandValidator : AbstractValidator<AuthVerifyEmailCommand>
{
    private readonly IIdentityService _identityService;

    public AuthVerifyEmailCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(EmailExists)
            .WithMessage("Email not found.");

        RuleFor(x => x.Token)
            .NotEmpty()
            .MinimumLength(20)
            .WithMessage("Token is invalid.");

    }

    private Task<bool> EmailExists(string email, CancellationToken cancellationToken)
    {
        return _identityService.CheckEmailExistsAsync(email, cancellationToken);
    }
}