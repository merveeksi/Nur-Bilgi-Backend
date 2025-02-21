using Microsoft.AspNetCore.Identity;
using NurBilgi.Domain.Common.Entities;

namespace NurBilgi.Domain.Identity;

public sealed class ApplicationUser: IdentityUser<long>, ICreatedByEntity, IModifiedByEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public String? ProfilePictureUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? Bio { get; set; }
    public DateTimeOffset LastOnLine { get; set; }
    public string CreatedByUserId { get; set; }
    public DateTimeOffset CreatedOn { get; set; }

    public string? ModifiedByUserId { get; set; }
    public DateTimeOffset? ModifiedOn { get; set; }
}
