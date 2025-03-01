using MediatR;
using NurBilgi.Application.Common.Attiributes;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Domain.Entities;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Application.Features.Favorites.Queries.GetAll
{
    public sealed record GetAllFavoritesQuery : IRequest<List<FavoriteGetAllDto>>, ICacheable
    {
        [CacheKeyPart]
        public long CustomerId { get; set; }
        public FavoriteContentType? ContentType { get; set; }

        public string CacheGroup => "Favorites";

        public GetAllFavoritesQuery(long customerId, FavoriteContentType? contentType = null)
        {
            CustomerId = customerId;
            ContentType = contentType;
        }
    }
}