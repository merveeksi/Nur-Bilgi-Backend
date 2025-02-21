using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Create;

public sealed class CreateAiChatMessageValidator : AbstractValidator<CreateAiChatMessageCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateAiChatMessageValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.MessageText)
            .NotEmpty()
            .WithMessage("MessageText is required")
            .MaximumLength(4000)
            .WithMessage("MessageText must be less than 4000 characters")
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("MessageText cannot be empty");

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required")
            .Must(x => _context.Customers.Any(c => c.Id == x))
            .WithMessage("Customer not found");

        RuleFor(x => x.Timestamp)
            .NotEmpty()
            .Must(x => x <= DateTimeOffset.UtcNow)
            .WithMessage("Timestamp must be in the past");

        RuleFor(x => x.IsCustomerMessage)
            .NotEmpty()
            .Must(x => x == true || x == false)
            .WithMessage("IsCustomerMessage must be true or false");
    }
}