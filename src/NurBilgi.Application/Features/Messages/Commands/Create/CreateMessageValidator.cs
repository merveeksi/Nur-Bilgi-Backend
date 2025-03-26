using FluentValidation;

namespace NurBilgi.Application.Features.Messages.Commands.Create;

public sealed class CreateMessageValidator : AbstractValidator<CreateMessageCommand>
{
    public CreateMessageValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("FullName is required")
            .MaximumLength(100)
            .WithMessage("FullName must be less than 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Invalid email address");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("Subject is required")
            .MaximumLength(100)
            .WithMessage("Subject must be less than 100 characters");

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content is required")
            .MaximumLength(4000)
            .WithMessage("Content must be less than 4000 characters")
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Content cannot be empty");

        RuleFor(x => x.SenderId)
            .NotEmpty()
            .WithMessage("SenderId is required")
            .MaximumLength(100)
            .WithMessage("SenderId must be less than 100 characters");

        RuleFor(x => x.ReceiverId)
            .NotEmpty()
            .WithMessage("ReceiverId is required")
            .MaximumLength(100)
            .WithMessage("ReceiverId must be less than 100 characters");
    }
} 