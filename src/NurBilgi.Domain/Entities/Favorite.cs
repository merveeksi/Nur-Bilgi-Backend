using System;
using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.Enum;

namespace NurBilgi.Domain.Entities;

public sealed class Favorite
{
    public long Id { get; set; }
    public FavoriteContentType ContentType { get; set; }
    public long ContentId { get; set; }
    // Foreign keys
    public long CustomerId { get; set; }
    
    // Navigation properties
    public Customer Customer { get; set; }
} 