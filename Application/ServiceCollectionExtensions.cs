using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    // Adds the application module to the service collection.
    // 
    // Parameters:
    //   services: The service collection to add the application module to.
    //
    // Returns:
    //   The service collection with the application module added.
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        return services.AddMediatR((c) =>
        {
            c.RegisterServicesFromAssembly(typeof(Application).Assembly);
        });
    }
}