using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;

namespace Consumer.Extensions;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BatchConsumer>(cfg =>
            {
                cfg.Options<BatchOptions>(options => options
                    .SetMessageLimit(1000)
                    .SetTimeLimit(s: 1)
                    .SetTimeLimitStart(BatchTimeLimitStart.FromLast)
                    .SetConcurrencyLimit(15));
            });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("async-pubSub", e =>
                {
                    e.AutoDelete = false;
                    e.Durable = true;
                    e.Exclusive = false;
                    e.DefaultContentType = new ContentType("application/json");
                    e.UseRawJsonDeserializer();

                    e.UseMessageRetry(r => r.Interval(5, 1000)); // this can be set to exponential

                    e.ConfigureConsumer<BatchConsumer>(context);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}
