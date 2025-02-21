using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Customers.Commands.Delete;

public sealed record DeleteCustomerCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }

    public DeleteCustomerCommand(long id)
    {
        Id = id;
    }
}
