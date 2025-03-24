using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Catechisms.Commands.Delete;

public record DeleteCatechismCommand(long Id) : IRequest<ResponseDto<long>>; 