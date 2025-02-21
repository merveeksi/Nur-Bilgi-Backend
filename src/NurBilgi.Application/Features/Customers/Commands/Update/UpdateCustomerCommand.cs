using MediatR;
using Microsoft.EntityFrameworkCore;
using NurBilgi.Application.Common.Interfaces;
using NurBilgi.Application.Common.Models.Responses;
using NurBilgi.Domain.ValueObjects;

namespace NurBilgi.Application.Features.Customers.Commands.Update;

public sealed record UpdateCustomerCommand(
    long Id,
    string UserName,  // ValueObject -> new UserName(UserName)
    string FirstName, // ValueObject -> new FullName(FirstName, LastName)
    string LastName,
    string Email,     // ValueObject -> new Email(Email)
    string Password   // ValueObject -> new Password(Password)
) : IRequest<ResponseDto<long>>;