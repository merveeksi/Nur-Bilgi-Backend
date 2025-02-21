using System;
using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Create;

public sealed class CreateDailyDuaValidator : AbstractValidator<CreateDailyDuaCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateDailyDuaValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.DuaText)
            .NotEmpty()
            .WithMessage("Dua text is required");

        RuleFor(x => x.ArabicText)
            .NotEmpty()
            .WithMessage("Arabic text is required");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Category is required")
            .Must(category => _context.DailyDuas.Any(d => d.Category == category))
            .WithMessage("Category must be a valid category")
            .MaximumLength(1000)
            .WithMessage("Category must be less than 1000 characters");

        RuleFor(x => x.Source)
            .NotEmpty()
            .WithMessage("Source is required")
            .MaximumLength(4000)
            .WithMessage("Source must be less than 4000 characters");

        RuleFor(x => x.TimeOfDay)
            .NotEmpty()
            .WithMessage("Time of day is required")
            .MaximumLength(1000)
            .WithMessage("Time of day must be less than 1000 characters");

    }

}
