using Consumer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddSingleton<IMongoRepository, MongoRepository>();
            return services;
        }
    }
}
