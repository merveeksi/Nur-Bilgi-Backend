using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Catechisms.Commands.Update;

public record UpdateCatechismCommand : IRequest<ResponseDto<long>>
{
    public long Id { get; set; }
    public string BookName { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Tags { get; set; } = string.Empty;
    
    public UpdateCatechismCommand(long id, string bookName, string authorName, string title, string description, string tags)
    {
        Id = id;
        BookName = bookName;
        AuthorName = authorName;
        Title = title;
        Description = description;
        Tags = tags;
    }

    public UpdateCatechismCommand()
    {
    }
} 