using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Favorites.Commands.Update;

public sealed class UpdateFavoriteCommandHandler 
    : IRequestHandler<UpdateFavoriteCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;

    public UpdateFavoriteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<long>> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

        if (favorite is null)
        {
            return ResponseDto<long>.Error("Favorite not found");
        }

        // Kullanıcı kendine ait bir Favorite kaydını mı güncelliyor?
        // Bu kontrolü yapmak isterseniz, favorite.CustomerId == request.CustomerId şeklinde ekleyebilirsiniz.

        favorite.ContentType = request.ContentType;
        favorite.ContentId = request.ContentId;
        favorite.CustomerId = request.CustomerId;

        await _context.SaveChangesAsync(cancellationToken);

        return ResponseDto<long>.Success(favorite.Id, "Favorite updated successfully");
    }
}