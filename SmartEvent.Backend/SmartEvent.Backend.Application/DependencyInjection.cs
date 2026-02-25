using Microsoft.Extensions.DependencyInjection;
using SmartEvent.Backend.Application.Interfaces.IServices;
using SmartEvent.Backend.Application.Services;

namespace SmartEvent.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}