using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Surahs.Commands.Update;

public sealed class UpdateSurahCommandHandler 
    : IRequestHandler<UpdateSurahCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdateSurahCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdateSurahCommand request, CancellationToken cancellationToken)
    {
        var surah = await _context.Surahs
            .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

        if (surah is null)
        {
            return ResponseDto<long>.Error("Surah not found");
        }

        surah.SurahNumber = request.SurahNumber;
        surah.Name = request.Name;
        surah.AyahCount = request.AyahCount;
        surah.ArabicText = request.ArabicText;
        surah.Translation = request.Translation;

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(surah.Id, "Surah updated successfully");
    }   
}