using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Surahs.Commands.Create;

public record CreateSurahCommand: IRequest<ResponseDto<long>>
{
    public int SurahNumber { get; set; }
    public string Name { get; set; } = string.Empty;
    public int AyahCount { get; set; }
    public string? ArabicText { get; set; }
    public string? Translation { get; set; }
    
    public CreateSurahCommand(string name, int surahNumber, int ayahCount, string? arabicText, string? translation)
    {
        Name = name;
        SurahNumber = surahNumber;
        AyahCount = ayahCount;
        ArabicText = arabicText;
        Translation = translation;
    }

    public CreateSurahCommand()
    {
    }
}