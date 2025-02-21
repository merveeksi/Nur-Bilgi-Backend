using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Delete;

public sealed class DeleteAiChatMessageCommandHandler : IRequestHandler<DeleteAiChatMessageCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public DeleteAiChatMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeleteAiChatMessageCommand request, CancellationToken cancellationToken)
    {
        var aiChatMessage = await _context.AiChatMessages
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        _context.AiChatMessages.Remove(aiChatMessage);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(aiChatMessage.Id, "Ai chat message deleted successfully", true);
    }
}