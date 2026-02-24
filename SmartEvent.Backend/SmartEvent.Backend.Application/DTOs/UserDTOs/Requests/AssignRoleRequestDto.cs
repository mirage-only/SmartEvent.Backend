using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Requests
{
    public class AssignRoleRequestDto
    {
        public Guid UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
