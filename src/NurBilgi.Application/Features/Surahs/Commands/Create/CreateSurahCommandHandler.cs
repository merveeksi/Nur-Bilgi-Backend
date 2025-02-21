using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.Surahs.Commands.Create;

public sealed class CreateSurahCommandHandler: IRequestHandler<CreateSurahCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateSurahCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateSurahCommand request, CancellationToken cancellationToken)
    {
        var surah = new Surah
        {
            SurahNumber = request.SurahNumber,
            Name = request.Name,
            AyahCount = request.AyahCount,
            ArabicText = request.ArabicText,
            Translation = request.Translation
        };

        _context.Surahs.Add(surah);

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Surahs", cancellationToken);

        return ResponseDto<long>.Success(surah.Id, "Sûre successfully created.");
    }
}