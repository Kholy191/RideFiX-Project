using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.AutoMapperProfile;


namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(ExampleProfile));
            return Services;
        }
    }
}
