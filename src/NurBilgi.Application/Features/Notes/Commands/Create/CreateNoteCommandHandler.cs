using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.Notes.Commands.Create;

public sealed class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateNoteCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = Note.Create(request.Title, request.Content, request.CustomerId);

         _context.Notes.Add(note);

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Notes", cancellationToken);

        return ResponseDto<long>.Success(note.Id, "Note created successfully.");
    }
}