using MediatR;

namespace NurBilgi.Application.Features.Favorites.Queries.GetById
{
    public sealed record FavoriteGetByIdQuery(long Id) : IRequest<FavoriteGetByIdDto>;
}