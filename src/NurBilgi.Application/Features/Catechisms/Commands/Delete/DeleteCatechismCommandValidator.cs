using FluentValidation;

namespace NurBilgi.Application.Features.Catechisms.Commands.Delete;

public sealed class DeleteCatechismCommandValidator : AbstractValidator<DeleteCatechismCommand>
{
    public DeleteCatechismCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("İlmihal ID'si boş olamaz.")
            .GreaterThan(0).WithMessage("İlmihal ID'si 0'dan büyük olmalıdır.");
    }
} 