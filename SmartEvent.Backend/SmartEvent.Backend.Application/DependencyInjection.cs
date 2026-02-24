using Microsoft.Extensions.DependencyInjection;

namespace SmartEvent.Backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}