using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        return services;
    }
}