using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Catechisms.Commands.Create;

public record CreateCatechismCommand : IRequest<ResponseDto<long>>
{
    public string BookName { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    
    public CreateCatechismCommand(string bookName, string authorName, string title, string description, string tags)
    {
        BookName = bookName;
        AuthorName = authorName;
        Title = title;
        Description = description;
        Tags = tags;
    }

    public CreateCatechismCommand()
    {
    }
} 