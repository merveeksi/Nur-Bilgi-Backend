using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Create;

public class CreateAiChatMessageCommandHandler : IRequestHandler<CreateAiChatMessageCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateAiChatMessageCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateAiChatMessageCommand request, CancellationToken cancellationToken)
    {
        var aiChatMessage = AiChatMessage.Create(request.MessageText, request.IsCustomerMessage, request.Timestamp, request.CustomerId);

        _context.AiChatMessages.Add(aiChatMessage);

        await _context.SaveChangesAsync(cancellationToken);
        
        await _cacheInvalidator.InvalidateGroupAsync("AiChatMessages", cancellationToken);

        return ResponseDto<long>.Success(aiChatMessage.Id, "Ai chat message created successfully");
    }
}