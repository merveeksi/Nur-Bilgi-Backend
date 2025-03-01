using MediatR;

namespace NurBilgi.Application.Features.DailyDuas.Queries.GetById
{
    public sealed record DailyDuaGetByIdQuery(long Id) : IRequest<DailyDuaGetByIdDto>;
}