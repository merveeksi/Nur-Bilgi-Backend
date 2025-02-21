using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Surahs.Commands.Update;

public sealed record UpdateSurahCommand(
    long Id,
    int SurahNumber,
    string Name,
    int AyahCount,
    string? ArabicText,
    string? Translation
) : IRequest<ResponseDto<long>>;