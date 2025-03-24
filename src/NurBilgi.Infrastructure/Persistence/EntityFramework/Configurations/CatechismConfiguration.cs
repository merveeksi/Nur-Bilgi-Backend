using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class CatechismConfiguration : IEntityTypeConfiguration<Catechism>
{
    public void Configure(EntityTypeBuilder<Catechism> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.BookName)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.AuthorName)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);
            
        builder.Property(x => x.Description)
            .IsRequired()
            .HasColumnType("text");
            
        builder.Property(x => x.Tags)
            .HasMaxLength(500);
            
        builder.Property(x => x.CreatedAt)
            .IsRequired(false);
            
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);
            
        // Create indexes to improve search performance
        builder.HasIndex(x => x.BookName);
        builder.HasIndex(x => x.AuthorName);
        builder.HasIndex(x => x.Tags);
        
        builder.ToTable("catechisms");
    }
} 