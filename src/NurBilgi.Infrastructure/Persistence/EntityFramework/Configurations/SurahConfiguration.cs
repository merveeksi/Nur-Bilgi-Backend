using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class SurahConfiguration : IEntityTypeConfiguration<Surah>
{
    public void Configure(EntityTypeBuilder<Surah> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.SurahNumber)
            .IsRequired();
            
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.AyahCount)
            .IsRequired();
            
        builder.Property(x => x.ArabicText)
            .HasMaxLength(10000);
            
        builder.Property(x => x.Translation)
            .HasMaxLength(10000);
            
        // Create a unique index on SurahNumber
        builder.HasIndex(x => x.SurahNumber)
            .IsUnique();
        
        builder.ToTable("surahs");
    }
} 