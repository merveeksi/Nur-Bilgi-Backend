using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Exceptions;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Catechisms.Commands.Delete;

public sealed class DeleteCatechismCommandHandler : IRequestHandler<DeleteCatechismCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public DeleteCatechismCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(DeleteCatechismCommand request, CancellationToken cancellationToken)
    {
        var catechism = await _context.Catechisms
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (catechism is null)
            throw new NotFoundException("Catechism", request.Id);

        _context.Catechisms.Remove(catechism);

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Catechisms", cancellationToken);

        return ResponseDto<long>.Success(catechism.Id, "Catechism successfully deleted.");
    }
} 