using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.Favorites.Commands.Create;

public sealed class CreateFavoriteCommandHandler: IRequestHandler<CreateFavoriteCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateFavoriteCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var favorite = new Favorite
        {
            ContentType = request.ContentType,
            ContentId = request.ContentId,
            CustomerId = request.CustomerId
        };

        _context.Favorites.Add(favorite);
        
        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Favorites", cancellationToken);

        return ResponseDto<long>.Success(favorite.Id, "Favorite created successfully.");
    }
}