using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Core.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public DateTime StartTime {  get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public Guid CreatorId { get; set; }
        public User Creator { get; set; }

        public QRCode QRCode { get; set; }

        public ICollection<Organizer> Organizers { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<QRCode> QRCodes { get; set; }

    }
}
