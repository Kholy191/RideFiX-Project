using Microsoft.Extensions.DependencyInjection;
using Service.AutoMapperProfile;
using Service.CoreServices.Account;
using ServiceAbstraction;
using ServiceAbstraction.CoreServicesAbstractions.Account;
using Services.AutoMapperProfile;


namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(typeof(RegisterMapping));
            Services.AddScoped<IFileService, FileService>();
            Services.AddHttpClient(); // تحضير الـ HttpClient في الـ DI
            Services.AddScoped<IFaceRecognitionService, FaceRecognitionService>(); Services.AddScoped<IAuthService, AuthService>();
            Services.AddMemoryCache();
            Services.AddScoped<IJwtService, JwtService>();





            return Services;
        }
    }
}
