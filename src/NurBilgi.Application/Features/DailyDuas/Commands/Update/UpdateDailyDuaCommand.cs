using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Update;

public sealed record UpdateDailyDuaCommand(
    long Id,
    string DuaText,
    string? ArabicText,
    string? Category,
    string? Source,
    string? TimeOfDay
) : IRequest<ResponseDto<long>>;
