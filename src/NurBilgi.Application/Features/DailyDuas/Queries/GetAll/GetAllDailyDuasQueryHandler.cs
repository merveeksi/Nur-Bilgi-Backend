using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.DailyDuas.Queries.GetAll
{
    public class GetAllDailyDuasQueryHandler : IRequestHandler<GetAllDailyDuasQuery, List<DailyDuaGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllDailyDuasQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DailyDuaGetAllDto>> Handle(GetAllDailyDuasQuery request, CancellationToken cancellationToken)
        {
            var query = _context.DailyDuas.AsQueryable();

            if (!string.IsNullOrEmpty(request.DuaText))
                query = query.Where(x => x.DuaText.ToLower().Contains(request.DuaText.ToLower()));

            if (!string.IsNullOrEmpty(request.Category))
                query = query.Where(x => x.Category == request.Category);

            if (!string.IsNullOrEmpty(request.TimeOfDay))
                query = query.Where(x => x.TimeOfDay == request.TimeOfDay);

            return await query.AsNoTracking()
                .Select(x => new DailyDuaGetAllDto(x.Id, x.DuaText, x.ArabicText, x.Category, x.Source, x.TimeOfDay))
                .ToListAsync(cancellationToken);
        }
    }
}