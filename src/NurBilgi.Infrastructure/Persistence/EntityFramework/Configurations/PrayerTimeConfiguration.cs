using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class PrayerTimeConfiguration : IEntityTypeConfiguration<PrayerTime>
{
    public void Configure(EntityTypeBuilder<PrayerTime> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(x => x.Date)
            .IsRequired();
            
        builder.Property(x => x.Fajr)
            .IsRequired();
            
        builder.Property(x => x.Dhuhr)
            .IsRequired();
            
        builder.Property(x => x.Asr)
            .IsRequired();
            
        builder.Property(x => x.Maghrib)
            .IsRequired();
            
        builder.Property(x => x.Isha)
            .IsRequired();
            
        builder.Property(x => x.Imsak)
            .IsRequired();
            
        // Create a unique index on City and Date
        builder.HasIndex(x => new { x.City, x.Date })
            .IsUnique();
        
        builder.ToTable("prayer-times");
    }
} 