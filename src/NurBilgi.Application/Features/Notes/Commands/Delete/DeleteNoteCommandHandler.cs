using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Notes.Commands.Delete;

public sealed class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public DeleteNoteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == request.Id, cancellationToken);

        _context.Notes.Remove(note);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(note.Id, "Note deleted successfully", true);
    }
} 