using Microsoft.AspNetCore.Http;
using SmartEvent.Backend.Application.Interfaces.ICommon;

namespace SmartEvent.Backend.Infrastructure.Security;

public class UserContext(IHttpContextAccessor httpContextAccessor): IUserContext
{
    private HttpContext? HttpContext => httpContextAccessor.HttpContext;

    public Guid UserId
    {
        get
        {
            var idClaim = HttpContext?.User.FindFirst("id")?.Value;
            return Guid.TryParse(idClaim, out var id) ? id : Guid.Empty;
        }
    }
    
    public string Email => HttpContext?.User.FindFirst("email")?.Value ?? string.Empty;

    public string IpAddress
    {
        get
        {
            if (HttpContext == null) return "Unknown";

            if (HttpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                var ip = forwardedFor.ToString().Split(',').FirstOrDefault()?.Trim();
                if (!string.IsNullOrEmpty(ip)) return ip;
            }
            
            return HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
        }
    }

    public string? UserAgent
    {
        get
        {
            var ua = HttpContext?.Request.Headers.UserAgent.ToString();
            return string.IsNullOrEmpty(ua) ? null : ua;
        }
    }
}