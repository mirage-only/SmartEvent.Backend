using SmartEvent.Backend.Core.Enums;
using SmartEvent.Backend.Core.Interfaces.IModels;

namespace SmartEvent.Backend.Core.Models
{
    public class Event: IAuditableModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartTime {  get; set; } = DateTime.UtcNow;
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Guid CreatorId { get; set; }
        public User? Creator { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        private const uint DefaultExpirationTime = 30; 
        
        public QrCode? CurrentQrCode => PastQrCodes
            .Where(qrCode => qrCode.Status == Status.Active && qrCode.ExpiredAt > DateTime.UtcNow)
            .OrderByDescending(qrCode => qrCode.CreatedAt)
            .FirstOrDefault();
        public uint QrCodeExpirationTime { get; set; } = DefaultExpirationTime;

        public ICollection<EventOrganizer> Organizers { get; set; } = [];
        public ICollection<Registration> Registrations { get; set; } = [];
        public ICollection<Attendance> Attendances { get; set; } = [];
        public ICollection<QrCode> PastQrCodes { get; set; } = [];
    }
}
