namespace SmartEvent.Backend.Core.Models
{
    public class EventOrganizer
    {
        public Guid EventId { get; init; }
        public Event? Event { get; set; }

        public Guid UserId { get; init; }
        public User? User { get; set; } 
        
        public DateTime AssignedAt { get; init; }
    }
}
