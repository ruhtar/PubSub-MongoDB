using Consumer.Options;
using Consumer.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddIOptionsImplementation(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<BcodexMongoOptions>(configuration.GetRequiredSection("BcodexMongoOptions"));
            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IMongoRepository, MongoRepository>();
            return services;
        }
    }
}
