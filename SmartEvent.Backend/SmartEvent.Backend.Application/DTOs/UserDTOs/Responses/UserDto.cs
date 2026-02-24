using SmartEvent.Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronymic { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
