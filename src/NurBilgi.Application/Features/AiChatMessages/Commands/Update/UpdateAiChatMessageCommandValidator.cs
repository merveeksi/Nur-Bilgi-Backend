using System;
using FluentValidation;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Update;

public sealed class UpdateAiChatMessageCommandValidator : AbstractValidator<UpdateAiChatMessageCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAiChatMessageCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) =>
            {
                var aiChatMessage = await _context.AiChatMessages.FindAsync(id);
                return aiChatMessage != null;
            })
            .WithMessage("AiChatMessage not found");

        RuleFor(x => x.MessageText)
            .NotEmpty()
            .WithMessage("MessageText is required")
            .MaximumLength(4000)
            .WithMessage("MessageText must be less than 4000 characters");

        RuleFor(x => x.IsCustomerMessage)
            .NotEmpty()
            .WithMessage("IsCustomerMessage is required");

        RuleFor(x => x.Timestamp)
            .NotEmpty()
            .WithMessage("Timestamp is required");

    }
}
