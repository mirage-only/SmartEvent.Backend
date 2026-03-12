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
}