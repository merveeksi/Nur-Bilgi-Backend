using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.DomainEvents;
using TSID.Creator.NET;

namespace NurBilgi.Domain.Entities;

public sealed class Message : EntityBase<long>
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string SenderId { get; set; } = string.Empty;
    public string ReceiverId { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public Message()
    {
    }

    public static Message Create(string fullName, string email, string subject, string content, string senderId, string receiverId)
    {
        var message = new Message
        {
            Id = TsidCreator.GetTsid().ToLong(),
            FullName = fullName,
            Email = email,
            Subject = subject,
            Content = content,
            SenderId = senderId,
            ReceiverId = receiverId,
            IsRead = false,
            CreatedAt = DateTimeOffset.UtcNow
        };
        
        message.RaiseDomainEvent(new MessageCreatedDomainEvent(message.Id));
        
        return message;
    }

    public void MarkAsRead()
    {
        IsRead = true;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Update(string fullName, string email, string subject, string content)
    {
        FullName = fullName;
        Email = email;
        Subject = subject;
        Content = content;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
} 