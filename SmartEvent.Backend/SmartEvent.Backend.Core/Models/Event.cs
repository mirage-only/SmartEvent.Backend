namespace SmartEvent.Backend.Core.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime StartTime {  get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Guid CreatorId { get; set; }
        public User? Creator { get; set; }

        public QrCode QrCode { get; set; }

        public ICollection<EventOrganizer> Organizers { get; set; } = [];
        public ICollection<Registration> Registrations { get; set; } = [];
        public ICollection<Attendance> Attendances { get; set; } = [];
        public ICollection<QrCode> PastQrCodes { get; set; } = [];
    }
}
