using Microsoft.AspNetCore.Identity;
using NurBilgi.Domain.Common.Entities;
using NurBilgi.Domain.DomainEvents;
using NurBilgi.Domain.ValueObjects;
using TSID.Creator.NET;

namespace NurBilgi.Domain.Identity;

public sealed class ApplicationUser: IdentityUserBase<long>, ICreatedByEntity, IModifiedByEntity
{
    public FullName FullName { get; set; }
    public String? ProfilePictureUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? Bio { get; set; }
    public DateTimeOffset LastOnLine { get; set; }
   
    public static ApplicationUser Create(FullName fullName, string email)
    {

        var id = TsidCreator
            .GetTsid()
            .ToLong();

        var user = new ApplicationUser
        {
            Id = id,
            FullName = fullName,
            Email = email,
            UserName = email,
            EmailConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var userRegistered = new UserRegisteredDomainEvent(id, email, fullName);

        user.RaiseDomainEvent(userRegistered);

        return user;
    }
}
