using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class AiChatMessageConfiguration : IEntityTypeConfiguration<AiChatMessage>
{
    public void Configure(EntityTypeBuilder<AiChatMessage> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.MessageText)
            .IsRequired()
            .HasMaxLength(4000);
            
        builder.Property(x => x.IsCustomerMessage)
            .IsRequired();
            
        builder.Property(x => x.Timestamp)
            .IsRequired();
        
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
            
        // Relationships
        builder.HasOne(x => x.Customer)
            .WithMany(u => u.AiChatMessages)
            .HasForeignKey(x => x.CustomerId);
        
        builder.ToTable("ai-chat-messages");
    }
} 