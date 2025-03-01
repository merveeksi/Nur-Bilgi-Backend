using NurBilgi.Domain.Enum;

namespace NurBilgi.Application.Features.Favorites.Queries.GetById
{
    public sealed record FavoriteGetByIdDto
    {
        public long Id { get; set; }
        public FavoriteContentType ContentType { get; set; }
        public long ContentId { get; set; }
        public long CustomerId { get; set; }

        public static FavoriteGetByIdDto Create(long id, FavoriteContentType contentType, long contentId, long customerId)
        {
            return new FavoriteGetByIdDto
            {
                Id = id,
                ContentType = contentType,
                ContentId = contentId,
                CustomerId = customerId
            };
        }

        public FavoriteGetByIdDto(long id, FavoriteContentType contentType, long contentId, long customerId)
        {
            Id = id;
            ContentType = contentType;
            ContentId = contentId;
            CustomerId = customerId;
        }

        public FavoriteGetByIdDto() { }
    }
}