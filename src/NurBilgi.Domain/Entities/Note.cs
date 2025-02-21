using System;
using NurBilgi.Domain.Common.Entities;

namespace NurBilgi.Domain.Entities;

public sealed class Note : EntityBase<long>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    
    // Foreign keys
    public long UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; }
} 