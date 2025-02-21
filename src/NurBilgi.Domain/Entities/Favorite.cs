using System;
using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Domain.Entities;

public sealed class Favorite : EntityBase<long>
{
    public FavoriteContentType ContentType { get; set; }
    public long ContentId { get; set; }
    // Foreign keys
    public long UserId { get; set; }
    
    // Navigation properties
    public User User { get; set; }
} 