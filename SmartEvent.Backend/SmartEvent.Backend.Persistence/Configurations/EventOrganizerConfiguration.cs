using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Configurations;

public class EventOrganizerConfiguration : IEntityTypeConfiguration<EventOrganizer>
{
    public void Configure(EntityTypeBuilder<EventOrganizer> builder)
    {
        builder.HasKey(eventOrganizer => new { eventOrganizer.UserId, eventOrganizer.EventId });

        builder.HasIndex(eventOrganizer => eventOrganizer.UserId);
        builder.HasIndex(eventOrganizer => eventOrganizer.EventId);
        
        builder
            .HasOne(eventOrganizer => eventOrganizer.Event)
            .WithMany(@event => @event.Organizers)
            .HasForeignKey(eventOrganizer => eventOrganizer.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(eventOrganizer => eventOrganizer.User)
            .WithMany(user => user.OrganizedEvents)
            .HasForeignKey(eventOrganizer => eventOrganizer.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}