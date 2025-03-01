using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetById;

public sealed class AiChatMessageGetByIdQueryHandler : IRequestHandler<AiChatMessageGetByIdQuery, AiChatMessageGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public AiChatMessageGetByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AiChatMessageGetByIdDto> Handle(AiChatMessageGetByIdQuery request, CancellationToken cancellationToken)
    {
        var aiChatMessage = await _context
        .AiChatMessages
        .AsNoTracking()
        .Select(x => new AiChatMessageGetByIdDto(x.Id, x.MessageText, x.IsCustomerMessage, x.Timestamp, x.CustomerId))
        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return aiChatMessage!;
    }
}