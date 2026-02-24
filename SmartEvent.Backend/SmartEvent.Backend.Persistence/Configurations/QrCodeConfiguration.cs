using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Persistence.Configurations;

public class QrCodeConfiguration: IEntityTypeConfiguration<QrCode>
{
    public void Configure(EntityTypeBuilder<QrCode> builder)
    {
        builder.HasKey(qrCode => qrCode.Id);
        
        builder.Property(qrCode => qrCode.TokenValue).IsRequired();
        builder.HasIndex(qrCode => qrCode.TokenValue).IsUnique();

        builder.HasIndex(qrCode => qrCode.EventId);
        
        builder
            .HasOne(qrCode => qrCode.Event)
            .WithMany(@event => @event.PastQrCodes)
            .HasForeignKey(qrCode => qrCode.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}