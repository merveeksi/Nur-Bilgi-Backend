using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.AiChatMessages.Commands.Update;

public record UpdateAiChatMessageCommand : IRequest
{
    public int Id { get; init; }
    public string Message { get; init; }
    public string Response { get; init; }
}

public class UpdateAiChatMessageCommandHandler : IRequestHandler<UpdateAiChatMessageCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAiChatMessageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateAiChatMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.AiChatMessages
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

     /*   if (entity == null)
        {
            throw new KeyNotFoundException($"AiChatMessage with ID {request.Id} not found.");
        }

        entity.Message = request.Message;
        entity.Response = request.Response;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);*/
    }
} 