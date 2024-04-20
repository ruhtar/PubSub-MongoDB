using Microsoft.Extensions.DependencyInjection;

namespace Publisher
{
    internal class ExtensionMethods
    {
        public IServiceCollection AddMassTransit(IServiceCollection services)
        {

            return services;
        }
    }
}
