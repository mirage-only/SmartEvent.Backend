using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Core.Models
{
    public class QrCode
    {
        public Guid Id { get; set; }

        public string TokenValue { get; set; } =  string.Empty;

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }

        public Guid EventId { get; set; }
        public Event? Event { get; set; }
    }
}
