using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Exceptions;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Catechisms.Queries.GetById
{
    public sealed class CatechismGetByIdQueryHandler : IRequestHandler<CatechismGetByIdQuery, CatechismGetByIdDto>
    {
        private readonly IApplicationDbContext _context;

        public CatechismGetByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CatechismGetByIdDto> Handle(CatechismGetByIdQuery request, CancellationToken cancellationToken)
        {
            var catechism = await _context.Catechisms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (catechism is null)
                throw new NotFoundException("Catechism", request.Id);

            return new CatechismGetByIdDto(
                catechism.Id, 
                catechism.BookName, 
                catechism.AuthorName,
                catechism.Title, 
                catechism.Description, 
                catechism.Tags, 
                catechism.CreatedAt ?? DateTimeOffset.UtcNow, 
                catechism.UpdatedAt ?? DateTimeOffset.UtcNow);
        }
    }
} 