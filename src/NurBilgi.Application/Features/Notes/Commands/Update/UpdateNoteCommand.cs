using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Notes.Commands.Update;

public sealed record UpdateNoteCommand(
    long Id,
    string Title,
    string Content,
    long CustomerId
) : IRequest<ResponseDto<long>>;
