using MediatR;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Customers.Commands.Create;

public record CreateCustomerCommand : IRequest<ResponseDto<long>>
{
    public UserName UserName { get; set; }
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public Password PasswordHash { get; set; }

    public CreateCustomerCommand(UserName userName, FullName fullName, Email email, Password passwordHash)
    {
        UserName = userName;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public CreateCustomerCommand()
    {
    }
}