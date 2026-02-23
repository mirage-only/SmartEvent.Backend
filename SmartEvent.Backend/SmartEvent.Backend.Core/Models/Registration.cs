namespace SmartEvent.Backend.Core.Models
{
    public class Registration
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        
        public Guid UserId { get; init; }
        public User? User { get; set; }

        public Guid EventId { get; init; }
        public Event? Event { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public bool IsCancelled { get; set; } = false;
    }
}
