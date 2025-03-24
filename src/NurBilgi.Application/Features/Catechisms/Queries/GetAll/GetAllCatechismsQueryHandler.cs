using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;

namespace NurBilgi.Application.Features.Catechisms.Queries.GetAll
{
    public class GetAllCatechismsQueryHandler : IRequestHandler<GetAllCatechismsQuery, List<CatechismGetAllDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCatechismsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CatechismGetAllDto>> Handle(GetAllCatechismsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Catechisms.AsQueryable();

            if (!string.IsNullOrEmpty(request.BookName))
                query = query.Where(x => x.BookName.ToLower().Contains(request.BookName.ToLower()));

            if (!string.IsNullOrEmpty(request.AuthorName))
                query = query.Where(x => x.AuthorName.ToLower().Contains(request.AuthorName.ToLower()));

            if (!string.IsNullOrEmpty(request.Tags))
                query = query.Where(x => x.Tags.ToLower().Contains(request.Tags.ToLower()));

            return await query.AsNoTracking()
                .Select(x => new CatechismGetAllDto(
                    x.Id, 
                    x.BookName, 
                    x.AuthorName,
                    x.Title, 
                    x.Description, 
                    x.Tags, 
                    x.CreatedAt ?? DateTimeOffset.UtcNow, 
                    x.UpdatedAt ?? DateTimeOffset.UtcNow))
                .ToListAsync(cancellationToken);
        }
    }
} 