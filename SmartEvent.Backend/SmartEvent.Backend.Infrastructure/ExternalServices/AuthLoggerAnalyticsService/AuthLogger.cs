using MassTransit;
using SmartEvent.Backend.Application.Interfaces.ICommon;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Shared.Contracts.Analytics;
using SmartEvent.Backend.Shared.Contracts.Analytics.Enums;
using SmartEvent.Backend.Shared.Contracts.AnalyticsEvents;

namespace SmartEvent.Backend.Infrastructure.ExternalServices.AuthLoggerAnalyticsService;

public class AuthLogger(IUserContext userContext, IPublishEndpoint publishEndpoint): IAuthLogger
{
    public async Task LogAuthAsync(string email, Guid? userId, AuthStatusEnum status, string? failureReason = null)
    {
        var logEvent = new AuthLogEvent
        {
            UserEmail = email,
            UserId = userId,
            Status = status,
            FailureReason = failureReason,
            IpAddress = userContext.IpAddress,
            UserAgent = userContext.UserAgent
        };
        
        await publishEndpoint.Publish(logEvent);
    }
}