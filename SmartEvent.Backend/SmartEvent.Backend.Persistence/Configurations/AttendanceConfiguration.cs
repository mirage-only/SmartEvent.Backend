using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Configurations;

public class AttendanceConfiguration: IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.HasKey(attendance => attendance.Id);

        builder.Property(attendance => attendance.ConfirmedAt).IsRequired();
        builder.Property(attendance => attendance.Method).IsRequired();
        
        builder
            .HasOne(attendance => attendance.User)
            .WithMany(user => user.Attendances)
            .HasForeignKey(attendance => attendance.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(attendance => attendance.Event)
            .WithMany(@event => @event.Attendances)
            .HasForeignKey(attendance => attendance.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}