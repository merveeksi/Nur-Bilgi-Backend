using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.DailyDuas.Queries.GetById
{
    public sealed class DailyDuaGetByIdQueryHandler : IRequestHandler<DailyDuaGetByIdQuery, DailyDuaGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public DailyDuaGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DailyDuaGetByIdDto> Handle(DailyDuaGetByIdQuery request, CancellationToken cancellationToken)
        {
            var duaDto = await _context.DailyDuas
                .AsNoTracking()
                .Select(x => new DailyDuaGetByIdDto(x.Id, x.DuaText, x.ArabicText, x.Category, x.Source, x.TimeOfDay))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return duaDto!;
        }
    }
}