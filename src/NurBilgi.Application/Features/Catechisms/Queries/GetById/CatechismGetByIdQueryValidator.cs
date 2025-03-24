using FluentValidation;

namespace NurBilgi.Application.Features.Catechisms.Queries.GetById;

public sealed class CatechismGetByIdQueryValidator : AbstractValidator<CatechismGetByIdQuery>
{
    public CatechismGetByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("İlmihal ID'si boş olamaz.")
            .GreaterThan(0).WithMessage("İlmihal ID'si 0'dan büyük olmalıdır.");
    }
} 