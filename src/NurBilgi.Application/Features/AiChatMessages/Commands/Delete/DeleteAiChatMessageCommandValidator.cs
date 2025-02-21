using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Translations.Validations;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Delete;

public sealed class DeleteAiChatMessageCommandValidator : AbstractValidator<DeleteAiChatMessageCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IStringLocalizer<ValidationTranslations> _localizer;
    public DeleteAiChatMessageCommandValidator(IApplicationDbContext context, IStringLocalizer<ValidationTranslations> localizer)
    {
        _context = context;
        _localizer = localizer;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(_localizer[ValidationTranslationKeys.AiChatMessageDeleteIdRequired])
            .MustAsync(AiChatMessageExist)
            .WithMessage(_localizer[ValidationTranslationKeys.AiChatMessageDeleteIdNotFound]);
    }

    private Task<bool> AiChatMessageExist(long id, CancellationToken cancellationToken)
    {
        return _context.AiChatMessages
        .AsNoTracking()
        .AnyAsync(a => a.Id == id, cancellationToken);
    }
}