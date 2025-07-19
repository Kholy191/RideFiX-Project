using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Presistence.unitofwork;
using ServiceAbstraction;


namespace Presistence
{
    public static class PresistenceLayerConfigurations
    {
        public static IServiceCollection AddPresistenceConfig(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        _configuration.GetConnectionString("DefaultConnection"),
                          sqlOptions => sqlOptions.EnableRetryOnFailure()
                    ));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
