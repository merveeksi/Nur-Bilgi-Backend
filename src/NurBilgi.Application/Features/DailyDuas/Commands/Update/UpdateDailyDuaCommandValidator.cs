using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Update;

public sealed class UpdateDailyDuaCommandValidator : AbstractValidator<UpdateDailyDuaCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDailyDuaCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) => 
            {
                return await _context.DailyDuas.AnyAsync(d => d.Id == id && !d.IsDeleted, ct);
            })
            .WithMessage("DailyDua not found");

        RuleFor(x => x.DuaText)
            .NotEmpty().WithMessage("DuaText is required");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required");

        RuleFor(x => x.Source)
            .NotEmpty().WithMessage("Source is required");

        RuleFor(x => x.TimeOfDay)
            .NotEmpty().WithMessage("TimeOfDay is required");
    }
}