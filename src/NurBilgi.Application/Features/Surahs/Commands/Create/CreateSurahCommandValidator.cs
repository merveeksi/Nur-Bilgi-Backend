using FluentValidation;

namespace NurBilgi.Application.Features.Surahs.Commands.Create;

public sealed class CreateSurahCommandValidator: AbstractValidator<CreateSurahCommand>
{
    public CreateSurahCommandValidator()
    {
        RuleFor(x => x.SurahNumber)
            .GreaterThan(0).WithMessage("Sûre numarası 0'dan büyük olmalıdır.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Sûre adı boş olamaz.")
            .MaximumLength(200).WithMessage("Sûre adı en fazla 200 karakter olabilir.");

        RuleFor(x => x.AyahCount)
            .GreaterThan(0).WithMessage("Ayet sayısı 0'dan büyük olmalıdır.");
    }
}