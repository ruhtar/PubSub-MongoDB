using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher;

public static class ExtensionMethods
{
    public static IServiceCollection AddHangfire(this IServiceCollection services)
    {
        services.AddHangfireServer();
        Console.WriteLine("Hangfire Server started. Press any key to exit...");
        GlobalConfiguration.Configuration.UseInMemoryStorage();
        return services;
    }
}
