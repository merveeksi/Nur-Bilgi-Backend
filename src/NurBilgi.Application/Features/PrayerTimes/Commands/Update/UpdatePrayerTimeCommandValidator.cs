using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Update;

public sealed class UpdatePrayerTimeCommandValidator : AbstractValidator<UpdatePrayerTimeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePrayerTimeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, ct) =>
            {
                return await _context.PrayerTimes.AnyAsync(pt => pt.Id == id, ct);
            })
            .WithMessage("PrayerTime not found");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required");

        RuleFor(x => x.Fajr)
            .NotEmpty().WithMessage("Fajr is required");

        RuleFor(x => x.Dhuhr)
            .NotEmpty().WithMessage("Dhuhr is required");

        RuleFor(x => x.Asr)
            .NotEmpty().WithMessage("Asr is required");

        RuleFor(x => x.Maghrib)
            .NotEmpty().WithMessage("Maghrib is required");

        RuleFor(x => x.Isha)
            .NotEmpty().WithMessage("Isha is required");

        RuleFor(x => x.Imsak)
            .NotEmpty().WithMessage("Imsak is required");
    
    }
}