using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetAll;

public class GetAllAiChatMessagesQueryHandler : IRequestHandler<GetAllAiChatMessagesQuery, List<AiChatMessageGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllAiChatMessagesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<AiChatMessageGetAllDto>> Handle(GetAllAiChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AiChatMessages.AsQueryable();

        query = query.Where(x => x.CustomerId == request.CustomerId);

        if (!string.IsNullOrEmpty(request.MessageText))
            query = query.Where(x => x.MessageText
            .ToLower()
            .Contains(request.MessageText.ToLower()));

        return query
        .AsNoTracking()
        .Select(x => new AiChatMessageGetAllDto(x.Id, x.MessageText, x.IsCustomerMessage, x.Timestamp))
        .ToListAsync(cancellationToken);
    }
}
