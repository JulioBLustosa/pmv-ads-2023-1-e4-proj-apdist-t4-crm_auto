using CRMobil.Entities;
using CRMobil.Interfaces;
using CRMobil.Services;
using MongoDB.Driver.Core.Configuration;

namespace CRMobil.IoC
{
    public static class InjectionDependency
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IServicosOficinaServices, ServicosOficinaServices>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionString>(configuration.GetSection("ConnectionStrings"));
            services.Configure<CnnStoreDatabaseSettings>(configuration.GetSection("ConnStoreDatabase"));

            return services;
        }
    }
}
