using SmartEvent.Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Core.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public DateTime ConfirmedAt { get; set; }

        public AttendanceMethod Method { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

    }
}
