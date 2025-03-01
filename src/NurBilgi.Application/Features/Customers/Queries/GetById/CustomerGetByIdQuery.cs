using MediatR;

namespace NurBilgi.Application.Features.Customers.Queries.GetById
{
    public sealed record CustomerGetByIdQuery(long Id) : IRequest<CustomerGetByIdDto>;
}