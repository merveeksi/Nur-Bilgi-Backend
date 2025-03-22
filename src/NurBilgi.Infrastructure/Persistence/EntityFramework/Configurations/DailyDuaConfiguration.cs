using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class DailyDuaConfiguration : IEntityTypeConfiguration<DailyDua>
{
    public void Configure(EntityTypeBuilder<DailyDua> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.DuaText)
            .IsRequired()
            .HasMaxLength(2000);
            
        builder.Property(x => x.ArabicText)
            .HasMaxLength(2000);
            
        builder.Property(x => x.Category)
            .HasMaxLength(100);
            
        builder.Property(x => x.Source)
            .HasMaxLength(200);
            
        builder.Property(x => x.TimeOfDay)
            .HasMaxLength(50);
        
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
        
        builder.ToTable("daily_dua");
    }
} 