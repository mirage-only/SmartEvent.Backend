namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Requests
{
    public class LoginUserRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
