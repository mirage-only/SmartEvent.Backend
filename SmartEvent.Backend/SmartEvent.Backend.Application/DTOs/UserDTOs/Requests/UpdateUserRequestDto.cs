using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Requests
{
    public class UpdateUserRequestDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Email { get; set; }
        public UserRole? UserRole { get; set; }
        public bool? IsActive { get; set; }
    }

}
