using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Configurations;

public class RegistrationConfiguration : IEntityTypeConfiguration<Registration>
{
    public void Configure(EntityTypeBuilder<Registration> builder)
    {
        builder.HasKey(registration => registration.Id);

        builder.HasIndex(registration => registration.EventId);
        builder.Property(registration => registration.EventId).IsRequired();
        
        builder.Property(registration => registration.UserId).IsRequired();
        
        builder.Property(registration => registration.IsCancelled).IsRequired();
        builder.Property(registration => registration.RegisteredAt).IsRequired();

        builder
            .HasOne(registration => registration.User)
            .WithMany(user => user.Registrations)
            .HasForeignKey(registration => registration.UserId);
        
        builder
            .HasOne(registration => registration.Event)
            .WithMany(@event => @event.Registrations)
            .HasForeignKey(registration => registration.EventId);
    }
}