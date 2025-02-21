using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Notes.Commands.Create;

public record CreateNoteCommand: IRequest<ResponseDto<long>>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public long CustomerId { get; set; }

    public CreateNoteCommand(string title, string content, long customerId)
    {
        Title = title;
        Content = content;
        CustomerId = customerId;
    }

    public CreateNoteCommand()
    {
    }
}
