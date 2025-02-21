using MediatR;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Application.Features.Favorites.Commands.Update;

public sealed record UpdateFavoriteCommand(
    long Id,
    FavoriteContentType ContentType,
    long ContentId,
    long CustomerId
) : IRequest<ResponseDto<long>>;