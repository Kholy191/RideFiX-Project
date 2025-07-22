using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Service.AutoMapperProfile;
using Service.CoreServices;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions;


namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<IRequestServices, RequstServices>();
            Services.AddScoped<ITechnicianService, TechnicianService>();
            Services.AddAutoMapper(typeof(PreRequestMapConfig));
            return Services;
        }
    }
}
