using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Exceptions;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Catechisms.Commands.Update;

public sealed class UpdateCatechismCommandHandler : IRequestHandler<UpdateCatechismCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public UpdateCatechismCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(UpdateCatechismCommand request, CancellationToken cancellationToken)
    {
        var catechism = await _context.Catechisms
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (catechism is null)
            throw new NotFoundException("Catechism", request.Id);

        catechism.BookName = request.BookName;
        catechism.AuthorName = request.AuthorName;
        catechism.Title = request.Title;
        catechism.Description = request.Description;
        catechism.Tags = request.Tags;
        catechism.UpdatedAt = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Catechisms", cancellationToken);

        return ResponseDto<long>.Success(catechism.Id, "Catechism successfully updated.");
    }
} 