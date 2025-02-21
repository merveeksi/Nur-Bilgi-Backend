using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.AiChatMessages.Queries.GetAll;

public record GetAllAiChatMessagesQuery : IRequest<List<AiChatMessageDto>>;

public class GetAllAiChatMessagesQueryHandler : IRequestHandler<GetAllAiChatMessagesQuery, List<AiChatMessageDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllAiChatMessagesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public Task<List<AiChatMessageDto>> Handle(GetAllAiChatMessagesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class AiChatMessageDto
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string Response { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
} 