using MediatR;

namespace NurBilgi.Application.Features.Surahs.Queries.GetById
{
    public sealed record SurahGetByIdQuery(long Id) : IRequest<SurahGetByIdDto>;
}