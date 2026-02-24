using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(user => user.Email).HasMaxLength(256).IsRequired();
        
        builder.Property(user => user.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(user => user.LastName).HasMaxLength(50).IsRequired();
        builder.Property(user => user.Patronymic).HasMaxLength(50).IsRequired();
        builder.Property(user => user.PasswordHash).IsRequired();
        builder.Property(user => user.UserRole).IsRequired();
        builder.Property(user => user.IsActive).IsRequired();

        builder
            .HasMany(user => user.OrganizedEvents)
            .WithOne(eventOrganizer => eventOrganizer.User)
            .HasForeignKey(user => user.UserId);
        
        builder
            .HasMany(user => user.Registrations)
            .WithOne(registration => registration.User)
            .HasForeignKey(user => user.UserId);
        
        builder
            .HasMany(user => user.Attendances)
            .WithOne(attendance => attendance.User)
            .HasForeignKey(user => user.UserId);
    }
}