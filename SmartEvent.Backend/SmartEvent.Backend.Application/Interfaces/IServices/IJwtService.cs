using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Application.Interfaces.IServices;

public interface IJwtService
{
    public string GenerateJwtToken(User user);
}