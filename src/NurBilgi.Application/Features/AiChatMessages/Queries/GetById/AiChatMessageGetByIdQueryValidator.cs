using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetById;

public sealed class AiChatMessageGetByIdQueryValidator : AbstractValidator<AiChatMessageGetByIdQuery>
{
    private readonly IApplicationDbContext _context;

    public AiChatMessageGetByIdQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Id)
        .NotEmpty()
        .MustAsync(CheckIfAiChatMessageExists)
        .WithMessage("AiChatMessage not found");
    }

    private  Task<bool> CheckIfAiChatMessageExists(long id, CancellationToken cancellationToken)
    {
        return  _context
        .AiChatMessages
        .AsNoTracking()
        .AnyAsync(x => x.Id == id, cancellationToken);
    }
}