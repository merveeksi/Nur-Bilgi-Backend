using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Surahs.Commands.Update;

public sealed class UpdateSurahCommandValidator : AbstractValidator<UpdateSurahCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSurahCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) =>
            {
                return await _context.Surahs.AnyAsync(s => s.Id == id, ct);
            })
            .WithMessage("Surah not found");

        RuleFor(x => x.SurahNumber)
            .GreaterThan(0).WithMessage("SurahNumber must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.AyahCount)
            .GreaterThan(0).WithMessage("AyahCount must be greater than 0");

        RuleFor(x => x.ArabicText)
            .NotEmpty().WithMessage("ArabicText is required");

        RuleFor(x => x.Translation)
            .NotEmpty().WithMessage("Translation is required");

    }
}