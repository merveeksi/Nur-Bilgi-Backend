using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Surahs.Queries.GetAll
{
    public class GetAllSurahsQueryHandler : IRequestHandler<GetAllSurahsQuery, List<SurahGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllSurahsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SurahGetAllDto>> Handle(GetAllSurahsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Surahs.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
                query = query.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));

            return await query.AsNoTracking()
                .Select(x => new SurahGetAllDto(x.Id, x.SurahNumber, x.Name, x.AyahCount, x.ArabicText, x.Translation))
                .ToListAsync(cancellationToken);
        }
    }
}