namespace SmartEvent.Backend.Application.Interfaces.ICommon;

public interface IUserContext
{
    Guid UserId { get; }
    string Email { get; }
    
    string IpAddress { get; }
    string? UserAgent { get; }
}