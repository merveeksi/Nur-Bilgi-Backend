using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.PrayerTimes.Commands.Create;

public sealed class CreatePrayerTimeCommandValidator : AbstractValidator<CreatePrayerTimeCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePrayerTimeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City name cannot be empty.")
            .MaximumLength(100)
            .WithMessage("City name cannot be longer than 100 characters.");

        RuleFor(x => x.Date)
            .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.AddDays(-1))
            .WithMessage("Date cannot be too old (example validation).");

        RuleFor(x => x.Fajr)
            .NotEmpty()
            .WithMessage("Fajr is required")
            .Must(x => x >= TimeSpan.Zero)
            .WithMessage("Fajr must be greater than or equal to 0");    

        RuleFor(x => x.Dhuhr)
            .NotEmpty()
            .WithMessage("Dhuhr is required")
            .Must(x => x >= TimeSpan.Zero)
            .WithMessage("Dhuhr must be greater than or equal to 0");

        RuleFor(x => x.Asr)
            .NotEmpty()
            .WithMessage("Asr is required")
            .Must(x => x >= TimeSpan.Zero)
            .WithMessage("Asr must be greater than or equal to 0");

        RuleFor(x => x.Maghrib)
            .NotEmpty()
            .WithMessage("Maghrib is required")
            .Must(x => x >= TimeSpan.Zero)
            .WithMessage("Maghrib must be greater than or equal to 0");

        RuleFor(x => x.Isha)
            .NotEmpty()
            .WithMessage("Isha is required")
            .Must(x => x >= TimeSpan.Zero)
            .WithMessage("Isha must be greater than or equal to 0");

        RuleFor(x => x.Imsak)
            .NotEmpty()
            .WithMessage("Imsak is required")
            .Must(x => x >= TimeSpan.Zero)
            .WithMessage("Imsak must be greater than or equal to 0");
    }
}