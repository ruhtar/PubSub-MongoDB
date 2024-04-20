using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher;

public static class ExtensionMethods
{
    public static IServiceCollection AddHangfire(this IServiceCollection services)
    {
        GlobalConfiguration.Configuration.UseInMemoryStorage();
        return services;
    }
}
