using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Configurations;

public class EventConfiguration: IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(@event => @event.Id);
        
        builder.Property(@event => @event.Name).HasMaxLength(200).IsRequired();
        builder.Property(@event => @event.Description).HasMaxLength(2000).IsRequired();
        builder.Property(@event => @event.ImageUrl).IsRequired();
        builder.Property(@event => @event.StartTime).IsRequired();
        builder.Property(@event => @event.Latitude).IsRequired();
        builder.Property(@event => @event.Longitude).IsRequired();
        builder.Property(@event => @event.CurrentQrCode).IsRequired();
        builder.Property(@event => @event.QrCodeExpirationTime).IsRequired();

        builder
            .HasMany(@event => @event.Organizers)
            .WithOne(@event => @event.Event)
            .HasForeignKey(@event => @event.EventId);
        
        builder
            .HasMany(@event => @event.Registrations)
            .WithOne(registration => registration.Event)
            .HasForeignKey(registration => registration.EventId);
        
        builder
            .HasMany(@event => @event.PastQrCodes)
            .WithOne(qrCode => qrCode.Event)
            .HasForeignKey(qrCode => qrCode.EventId);
        
        builder
            .HasMany(@event => @event.Attendances)
            .WithOne(attendance => attendance.Event)
            .HasForeignKey(attendance => attendance.EventId);
    }
}