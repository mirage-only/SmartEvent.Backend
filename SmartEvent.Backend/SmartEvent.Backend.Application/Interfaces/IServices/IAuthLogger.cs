using SmartEvent.Backend.Shared.Contracts.Analytics.Enums;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IAuthLogger
{
    Task LogAuthAsync(string email, Guid? userId, AuthStatusEnum status, string? failureReason = null);
}