using System;
using System.Collections.Generic;
using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.DomainEvents;
using NurBilgi.Domain.ValueObjects;
using TSID.Creator.NET;

namespace NurBilgi.Domain.Entities;

public sealed class Customer : EntityBase<long>
{
    public UserName UserName { get; set; }
    public FullName FullName { get; set; }
    public Email Email { get; set; }
    public Password PasswordHash { get; set; }
    
    // Navigation properties
    public ICollection<Note> Notes { get; set; } = [];
    public ICollection<Favorite> Favorites { get; set; } = [];
    public ICollection<AiChatMessage> AiChatMessages { get; set; } = [];
    
    public Customer()
    {
        Notes = new HashSet<Note>();
        Favorites = new HashSet<Favorite>();
        AiChatMessages = new HashSet<AiChatMessage>();
    }

    public Customer(UserName userName, FullName fullName, Email email, Password passwordHash) : this()
    {
        UserName = userName;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }
} 