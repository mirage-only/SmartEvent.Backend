using SmartEvent.Backend.Core.Enums;

namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Responses
{
    public class AuthorizeUserResponseDto
    {
        public string JwtToken { get; set; } = string.Empty;
    }
}
