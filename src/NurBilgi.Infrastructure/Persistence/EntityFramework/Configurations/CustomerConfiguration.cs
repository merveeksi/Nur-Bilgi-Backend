using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NurBilgi.Domain.Entities;

namespace NurBilgi.Infrastructure.Persistence.EntityFramework.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.OwnsOne(x => x.UserName, userName =>
        {
            userName.Property(n => n.Value)
                .HasColumnName("user-name")
                .IsRequired()
                .HasMaxLength(50);
        });
        
        builder.OwnsOne(customer => customer.FullName, fullNameBuilder =>
        {
            fullNameBuilder.Property(fullName => fullName.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("first_name");

            fullNameBuilder.Property(fullName => fullName.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("last_name");
        });
        
        builder.OwnsOne(x => x.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("e-mail")
                .IsRequired()
                .HasMaxLength(256);
        });
        
        builder.OwnsOne(x => x.PasswordHash, password =>
        {
            password.Property(p => p.Value)
                .HasColumnName("password-hash")
                .IsRequired()
                .HasMaxLength(1000);
        });
        
        // Create unique indexes
        builder.HasIndex(x => x.Email)
            .IsUnique();
        builder.HasIndex(x => x.UserName)
            .IsUnique();
        
        builder.ToTable("users");
    }
} 