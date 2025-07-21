using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service.AutoMapperProfile;
using ServiceAbstraction;


namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(PreRequestMapConfig));
            return Services;
        }
    }
}
