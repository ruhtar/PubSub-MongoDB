using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace Consumer;

public static class ExtensionMethods
{
    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BatchConsumer>(cfg =>
            {
                cfg.Options<BatchOptions>(options => options
                    .SetMessageLimit(200)
                    .SetTimeLimit(s: 1)
                    .SetTimeLimitStart(BatchTimeLimitStart.FromLast)
                    .SetConcurrencyLimit(10));
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("queue-mass-transit", e =>
                {
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.Exclusive = false;
                    e.DefaultContentType = new ContentType("application/json");
                    e.UseRawJsonDeserializer();

                    e.UseMessageRetry(r => r.Interval(5, 1000));

                    e.ConfigureConsumer<BatchConsumer>(context);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
