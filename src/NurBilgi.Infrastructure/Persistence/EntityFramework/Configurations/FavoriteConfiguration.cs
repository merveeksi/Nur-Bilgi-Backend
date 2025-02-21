using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.ContentType)
            .IsRequired();
            
        builder.Property(x => x.ContentId)
            .IsRequired();
            
        builder.HasOne(x => x.Customer)
            .WithMany(u => u.Favorites)
            .HasForeignKey(x => x.CustomerId);
            
        // Create a unique index on UserId, ContentType, and ContentId
        builder.HasIndex(x => new { x.CustomerId, x.ContentType, x.ContentId })
            .IsUnique();
        
        builder.ToTable("favorites");
    }
} 