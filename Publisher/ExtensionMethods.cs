using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Publisher;

public static class ExtensionMethods
{
    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {

            });
        });
        return services;
    }
}
