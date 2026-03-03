namespace SmartEvent.Backend.Application.DTOs.UserDTOs.Responses;

public record GetUserDto(Guid UserId,  string Email, string FirstName, string LastName,
    string Patronymic, string Role, bool IsActive);