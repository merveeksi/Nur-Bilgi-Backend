using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Favorites.Queries.GetById
{
    public sealed class FavoriteGetByIdQueryHandler : IRequestHandler<FavoriteGetByIdQuery, FavoriteGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public FavoriteGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FavoriteGetByIdDto> Handle(FavoriteGetByIdQuery request, CancellationToken cancellationToken)
        {
            var favoriteDto = await _context.Favorites
                .AsNoTracking()
                .Select(x => new FavoriteGetByIdDto(x.Id, x.ContentType, x.ContentId, x.CustomerId))
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return favoriteDto!;
        }
    }
}