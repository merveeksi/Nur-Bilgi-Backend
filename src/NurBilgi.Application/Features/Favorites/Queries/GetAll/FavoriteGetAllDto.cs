using NurBilgi.Domain.Enum;

namespace NurBilgi.Application.Features.Favorites.Queries.GetAll
{
    public sealed record FavoriteGetAllDto
    {
        public long Id { get; set; }
        public FavoriteContentType ContentType { get; set; }
        public long ContentId { get; set; }
        public long CustomerId { get; set; }

        public FavoriteGetAllDto(long id, FavoriteContentType contentType, long contentId, long customerId)
        {
            Id = id;
            ContentType = contentType;
            ContentId = contentId;
            CustomerId = customerId;
        }

        public FavoriteGetAllDto() { }

        public static FavoriteGetAllDto Create(long id, FavoriteContentType contentType, long contentId, long customerId)
            => new FavoriteGetAllDto(id, contentType, contentId, customerId);
    }
}