using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Auth.Commands.Login;

public sealed class AuthLoginCommandValidator : AbstractValidator<AuthLoginCommand>
{
    private readonly IIdentityService _identityService;

    public AuthLoginCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(150).WithMessage("E-posta adresi 150 karakterden uzun olamaz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .MaximumLength(50).WithMessage("Şifre 50 karakterden uzun olamaz.");

        RuleFor(x => x)
            .MustAsync(BeValidUserAsync)
            .WithMessage("Geçersiz şifre veya e-posta");
    }

    private Task<bool> BeValidUserAsync(AuthLoginCommand model, CancellationToken cancellationToken)
    {
        var request = model.ToIdentityAuthenticateRequest();

        return _identityService.AuthenticateAsync(request, cancellationToken);
    }
}