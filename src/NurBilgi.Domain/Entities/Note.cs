using System;
using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.DomainEvents;
using TSID.Creator.NET;

namespace NurBilgi.Domain.Entities;

public sealed class Note : EntityBase<long>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    
    // Foreign keys
    public long CustomerId { get; set; }
    
    // Navigation properties
    public Customer Customer { get; set; }

    public static Note Create(string title, string content, long customerId)
    {
        var note = new Note
        {
            Id = TsidCreator.GetTsid().ToLong(),
            Title = title,
            Content = content,
            CustomerId = customerId,
        };

        note.RaiseDomainEvent(new NoteCreatedDomainEvent(note.Id));

        return note;
    }
} 