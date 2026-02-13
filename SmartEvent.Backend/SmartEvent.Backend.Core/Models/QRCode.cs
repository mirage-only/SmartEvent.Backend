using SmartEvent.Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Core.Models
{
    public class QRCode
    {
        public Guid Id { get; set; }

        public string TokenValue { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiredAt { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }
    }
}
