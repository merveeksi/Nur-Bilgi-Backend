using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(4000);
        
        // CreatedOn
        builder.Property(x => x.CreatedOn)
            .HasDefaultValue(DateTimeOffset.UtcNow)
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
        
        // Relationships
        builder.HasOne(x => x.Customer)
            .WithMany(u => u.Notes)
            .HasForeignKey(x => x.CustomerId);
        
        builder.ToTable("notes");
    }
} 