using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Surahs.Queries.GetById
{
    public sealed class SurahGetByIdQueryHandler : IRequestHandler<SurahGetByIdQuery, SurahGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public SurahGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SurahGetByIdDto> Handle(SurahGetByIdQuery request, CancellationToken cancellationToken)
        {
            var surahDto = await _context.Surahs
                .AsNoTracking()
                .Select(x => new SurahGetByIdDto(x.Id, x.SurahNumber, x.Name, x.AyahCount, x.ArabicText, x.Translation))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return surahDto!;
        }
    }
}