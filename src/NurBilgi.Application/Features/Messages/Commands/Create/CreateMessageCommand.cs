using MediatR;
using NurBilgi.Application.Common.Models.Responses;

namespace NurBilgi.Application.Features.Messages.Commands.Create;

public record CreateMessageCommand : IRequest<ResponseDto<long>>
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }

    public CreateMessageCommand(string fullName, string email, string subject, string content, string senderId, string receiverId)
    {
        FullName = fullName;
        Email = email;
        Subject = subject;
        Content = content;
        SenderId = senderId;
        ReceiverId = receiverId;
    }

    public CreateMessageCommand()
    {
    }
} 