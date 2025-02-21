using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.DailyDuas.Commands.Create;

public record CreateDailyDuaCommand : IRequest<ResponseDto<long>>
{
    public string DuaText { get; set; }
    public string? ArabicText { get; set; }
    public string? Category { get; set; }
    public string? Source { get; set; }
    public string? TimeOfDay { get; set; }

    public CreateDailyDuaCommand(string duaText, string? arabicText, string? category, string? source, string? timeOfDay)
    {
        DuaText = duaText;
        ArabicText = arabicText;
        Category = category;
        Source = source;
        TimeOfDay = timeOfDay;
    }

    public CreateDailyDuaCommand()
    {
    }
}
