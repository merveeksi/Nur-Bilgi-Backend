using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Auth.Commands.Register;

public sealed record AuthRegisterCommand(string Email, string Password, string FullName) : IRequest<ResponseDto<string>>;
