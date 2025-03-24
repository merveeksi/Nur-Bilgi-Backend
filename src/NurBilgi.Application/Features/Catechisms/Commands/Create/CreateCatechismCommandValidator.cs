using FluentValidation;

namespace NurBilgi.Application.Features.Catechisms.Commands.Create;

public sealed class CreateCatechismCommandValidator : AbstractValidator<CreateCatechismCommand>
{
    public CreateCatechismCommandValidator()
    {
        RuleFor(x => x.BookName)
            .NotEmpty().WithMessage("Kitap adı boş olamaz.")
            .MaximumLength(200).WithMessage("Kitap adı en fazla 200 karakter olabilir.");

        RuleFor(x => x.AuthorName)
            .NotEmpty().WithMessage("Yazar adı boş olamaz.")
            .MaximumLength(200).WithMessage("Yazar adı en fazla 200 karakter olabilir.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık en fazla 200 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.Tags)
            .MaximumLength(500).WithMessage("Etiketler en fazla 500 karakter olabilir.");
    }
} 