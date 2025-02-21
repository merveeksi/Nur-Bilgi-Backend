using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Favorites.Commands.Delete;

public sealed class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public DeleteFavoriteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        _context.Favorites.Remove(favorite);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<long>(favorite.Id, "Favorite deleted successfully", true);
    }
} 