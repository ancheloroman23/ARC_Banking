using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Infrastructure.Persistence.Repositories;
using ARC_InternetBanking.Infrastructure.Persistence.Contexts;

namespace ARC_InternetBanking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ArcConnectionBD"));
            }
            else
            {
                services.AddDbContext<ApplicationContext > (options =>
                options.UseSqlServer(configuration.GetConnectionString("ArcConnectionBD"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region Repositories

            services.AddTransient(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddTransient<IBeneficiarioRepository, BeneficiarioRepository>();
            services.AddTransient<IPrestamoRepository, PrestamoRepository>();
            services.AddTransient<ICuentaAhorroRepository, CuentaAhorroRepository>();
            services.AddTransient<ITransaccionRepository, TransaccionRepository>();
            services.AddTransient<ITarjetaCreditoRepository, TarjetaCreditoRepository>();

            #endregion
        }
    }
}
