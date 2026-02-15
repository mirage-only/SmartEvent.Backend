using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Core.Models
{
    public class Organizer
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
