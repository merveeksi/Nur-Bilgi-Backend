using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Features.AiChatMessages.Queries.GetAll;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetById;

public record GetAiChatMessageByIdQuery(long Id) : IRequest<AiChatMessageDto>;

public class GetAiChatMessageByIdQueryHandler : IRequestHandler<GetAiChatMessageByIdQuery, AiChatMessageDto>
{
    private readonly IApplicationDbContext _context;

    public GetAiChatMessageByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public Task<AiChatMessageDto> Handle(GetAiChatMessageByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
} 