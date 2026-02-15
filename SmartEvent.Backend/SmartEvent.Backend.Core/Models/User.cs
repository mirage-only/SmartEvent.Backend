using SmartEvent.Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public UserRole UserRole { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Organizer> OrganizedEvents { get; set; }
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
