using System;
using System.Collections.Generic;
using NurBilgi.Domain.Common.Entities;

namespace NurBilgi.Domain.Entities;

public sealed class User : EntityBase<long>
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<Note> Notes { get; set; } = [];
    public ICollection<Favorite> Favorites { get; set; } = [];
    public ICollection<AiChatMessage> AiChatMessages { get; set; } = [];
    
    public User()
    {
        Notes = new HashSet<Note>();
        Favorites = new HashSet<Favorite>();
        AiChatMessages = new HashSet<AiChatMessage>();
    }
} 