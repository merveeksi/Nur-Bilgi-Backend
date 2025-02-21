using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Surahs.Commands.Delete;

public sealed class DeleteSurahCommandHandler : IRequestHandler<DeleteSurahCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public DeleteSurahCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeleteSurahCommand request, CancellationToken cancellationToken)
    {
        var surah = await _context.Surahs
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        _context.Surahs.Remove(surah);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(surah.Id, "Surah deleted successfully", true);
    }
} 