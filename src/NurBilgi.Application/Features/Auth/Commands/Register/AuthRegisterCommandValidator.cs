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

        RuleFor(x => x.FullName)
            .NotNull()
            .WithMessage("Ad ve soyad gereklidir.");

        When(x => x.FullName != null, () =>
        {
            RuleFor(x => x.FullName.FirstName)
                .NotEmpty()
                .WithMessage("Ad alanı gereklidir")
                .MinimumLength(2)
                .WithMessage("Ad en az 2 karakter olmalıdır")
                .MaximumLength(50)
                .WithMessage("Ad en fazla 50 karakter olabilir")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")
                .WithMessage("Ad sadece harflerden oluşmalıdır");

            RuleFor(x => x.FullName.LastName)
                .NotEmpty()
                .WithMessage("Soyad alanı gereklidir")
                .MinimumLength(2)
                .WithMessage("Soyad en az 2 karakter olmalıdır")
                .MaximumLength(50)
                .WithMessage("Soyad en fazla 50 karakter olabilir")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ]+$")
                .WithMessage("Soyad sadece harflerden oluşmalıdır");
        });

        RuleFor(x => x.Email)
            .MustAsync(CheckEmailExistsAsync)
            .WithMessage("Bu e-posta adresi zaten kullanımda.");
    }

    private async Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return !await _identityService.CheckEmailExistsAsync(email, cancellationToken);
    }
}