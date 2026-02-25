using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Core.Models;

namespace SmartEvent.Backend.Infrastructure.Security.Jwt;

public class JwtService(IOptions<AuthSettings> options): IJwtService
{
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim("email", user.Email),
            new Claim("role", user.UserRole.ToString())
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}