using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Notes.Commands.Update;

public sealed class UpdateNoteCommandHandler 
    : IRequestHandler<UpdateNoteCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdateNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == request.Id && !n.IsDeleted, cancellationToken);

        if (note is null)
        {
            return ResponseDto<long>.Error("Note not found");
        }

        // Kullanıcı kendine ait bir Note kaydını mı güncelliyor?
        // Bu kontrolü yapmak isterseniz, note.CustomerId == request.CustomerId şeklinde ekleyebilirsiniz.

        note.Title = request.Title;
        note.Content = request.Content;
        note.CustomerId = request.CustomerId;

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(note.Id, "Note updated successfully");
    }
}