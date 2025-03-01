using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.Entities;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Application.Features.Favorites.Queries.GetAll
{
    public class GetAllFavoritesQueryHandler : IRequestHandler<GetAllFavoritesQuery, List<FavoriteGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllFavoritesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteGetAllDto>> Handle(GetAllFavoritesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Favorites.AsQueryable();

            query = query.Where(x => x.CustomerId == request.CustomerId);

            if (request.ContentType.HasValue)
                query = query.Where(x => x.ContentType == request.ContentType.Value);

            return await query.AsNoTracking()
                .Select(x => new FavoriteGetAllDto(x.Id, x.ContentType, x.ContentId, x.CustomerId))
                .ToListAsync(cancellationToken);
        }
    }
}