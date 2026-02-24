using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Responses
{
    public class AssignRoleResponseDto
    {
        public Guid UserId { get; set; }
        public UserRole Role { get; set; }
    }
}
