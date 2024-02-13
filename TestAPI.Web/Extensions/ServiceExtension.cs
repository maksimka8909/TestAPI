using TestAPI.Services;

namespace TestAPI.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddMyService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ClientService>();
        serviceCollection.AddScoped<FounderService>();

        return serviceCollection;
    }
}