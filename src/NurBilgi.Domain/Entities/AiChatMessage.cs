using NurBilgi.Domain.Common.Entities;

namespace NurBilgi.Domain.Entities;

public sealed class AiChatMessage : EntityBase<long>
{
    public string MessageText { get; set; } = string.Empty;
    public bool IsUserMessage { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    
    // Foreign keys
    public long UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; }
} 