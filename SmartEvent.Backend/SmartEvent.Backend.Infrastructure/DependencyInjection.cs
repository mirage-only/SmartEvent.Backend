using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Application.Services;
using SmartEvent.Backend.Core.Models;
using SmartEvent.Backend.Infrastructure.Security;
using SmartEvent.Backend.Infrastructure.Security.Extensions;
using SmartEvent.Backend.Infrastructure.Security.Jwt;
using SmartEvent.Backend.Infrastructure.Security.PasswordHasher;

namespace SmartEvent.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<PasswordHasher<User>>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();
        
        services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
        services.AddAuth(configuration);
        
        return services;
    }
}