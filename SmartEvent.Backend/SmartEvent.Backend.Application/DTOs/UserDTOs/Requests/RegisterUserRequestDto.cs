namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Requests
{
    public class RegisterUserRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
    }

}
