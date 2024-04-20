using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher;

public static class ExtensionMethods
{
    public static IServiceCollection AddHangfire(this IServiceCollection services)
    {
        services.AddHangfire();
        GlobalConfiguration.Configuration.UseInMemoryStorage();
        return services;
    }
}
