using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            return Services;
        }
    }
}
