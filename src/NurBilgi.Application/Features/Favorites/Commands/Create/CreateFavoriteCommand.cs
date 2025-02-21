using MediatR;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Application.Features.Favorites.Commands.Create;

public record CreateFavoriteCommand: IRequest<ResponseDto<long>>
{
    public FavoriteContentType ContentType { get; set; }
    public long ContentId { get; set; }
    public long CustomerId { get; set; }

    public CreateFavoriteCommand(FavoriteContentType contentType, long contentId, long customerId)
    {
        ContentType = contentType;
        ContentId = contentId;
        CustomerId = customerId;
    }

    public CreateFavoriteCommand()
    {
    }
}