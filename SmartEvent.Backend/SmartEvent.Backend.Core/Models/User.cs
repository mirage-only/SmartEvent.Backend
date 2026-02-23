using SmartEvent.Backend.Core.Enums;
using SmartEvent.Backend.Core.Interfaces.IModels;

namespace SmartEvent.Backend.Core.Models
{
    public class User: IAuditableModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public string Email { get; set; } = string.Empty;
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } =  string.Empty;
        public string Patronymic { get; set; } = string.Empty;

        public string PasswordHash { get; set; } =  string.Empty;

        public UserRole UserRole { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<EventOrganizer> OrganizedEvents { get; set; } = [];
        public ICollection<Registration> Registrations { get; set; } = [];
        public ICollection<Attendance> Attendances { get; set; } = [];
    }
}
