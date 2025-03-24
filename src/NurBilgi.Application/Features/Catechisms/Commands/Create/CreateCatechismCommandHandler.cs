using MediatR;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Application.Features.Catechisms.Commands.Create;

public sealed class CreateCatechismCommandHandler : IRequestHandler<CreateCatechismCommand, ResponseDto<long>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateCatechismCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<ResponseDto<long>> Handle(CreateCatechismCommand request, CancellationToken cancellationToken)
    {
        var catechism = new Catechism
        {
            BookName = request.BookName,
            AuthorName = request.AuthorName,
            Title = request.Title,
            Description = request.Description,
            Tags = request.Tags,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        _context.Catechisms.Add(catechism);

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("Catechisms", cancellationToken);

        return ResponseDto<long>.Success(catechism.Id, "Catechism successfully created.");
    }
} 