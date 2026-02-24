using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Responses
{
    public class AuthorizeUserResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty; 
        public string LastName { get; set; } = string.Empty; 
        public string Patronymic { get; set; } = string.Empty;
        public UserRole UserRole { get; set; }
    }
}
