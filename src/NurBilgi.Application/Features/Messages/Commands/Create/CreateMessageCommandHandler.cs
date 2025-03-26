using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.Messages.Commands.Create;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateMessageCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = Message.Create(
            request.FullName,
            request.Email,
            request.Subject,
            request.Content,
            request.SenderId,
            request.ReceiverId
        );

        _context.Messages.Add(message);

        await _context.SaveChangesAsync(cancellationToken);
        
        await _cacheInvalidator.InvalidateGroupAsync("Messages", cancellationToken);

        return ResponseDto<long>.Success(message.Id, "Message created successfully");
    }
} 