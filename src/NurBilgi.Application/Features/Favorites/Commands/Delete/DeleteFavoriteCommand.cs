using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Favorites.Commands.Delete;

public sealed class DeleteFavoriteCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeleteFavoriteCommand(long id)
    {
        Id = id;
    }
} 