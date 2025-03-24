using MediatR;

namespace NurBilgi.Application.Features.Catechisms.Queries.GetById
{
    public sealed record CatechismGetByIdQuery(long Id) : IRequest<CatechismGetByIdDto>;
} 