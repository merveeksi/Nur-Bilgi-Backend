using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Update;

public class UpdateAiChatMessageCommandHandler : IRequestHandler<UpdateAiChatMessageCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdateAiChatMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdateAiChatMessageCommand request, CancellationToken cancellationToken)
    {
        var aiChatMessage = await _context
            .AiChatMessages
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == request.Id && !a.IsDeleted, cancellationToken);

        if (aiChatMessage == null)
        {
            return ResponseDto<long>.Error("AiChatMessage not found");
        }

        aiChatMessage.MessageText = request.MessageText;
        aiChatMessage.IsCustomerMessage = request.IsCustomerMessage;
        aiChatMessage.Timestamp = request.Timestamp;
        aiChatMessage.CustomerId = request.CustomerId;

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(aiChatMessage.Id, "AiChatMessage updated successfully");
    }
} 