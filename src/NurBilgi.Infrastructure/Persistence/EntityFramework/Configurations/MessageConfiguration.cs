using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.FullName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Subject)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(x => x.SenderId)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.ReceiverId)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.IsRead)
            .IsRequired();
            
        builder.Property(x => x.CreatedAt)
            .IsRequired();
            
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        // CreatedOn
        builder.Property(x => x.CreatedOn)
            .HasDefaultValue(new DateTimeOffset(2025, 3, 22, 0, 0, 0, TimeSpan.Zero))
            .IsRequired();

        // CreatedByUserId
        builder.Property(x => x.CreatedByUserId)
            .HasMaxLength(100)
            .IsRequired(false);

        // ModifiedOn
        builder.Property(x => x.ModifiedOn)
            .IsRequired(false);

        // ModifiedByUserId
        builder.Property(x => x.ModifiedByUserId)
            .HasMaxLength(100)
            .IsRequired(false);
            
        builder.ToTable("messages");
    }
} 