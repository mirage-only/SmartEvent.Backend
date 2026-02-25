using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Application.Services;
using SmartEvent.Backend.Core.Models;
using SmartEvent.Backend.Infrastructure.Security;

namespace SmartEvent.Backend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<PasswordHasher<User>>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        
        return services;
    }
}