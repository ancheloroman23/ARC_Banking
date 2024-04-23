

using ARC_InternetBanking.Core.Application.Interfaces.Services;
using ARC_InternetBanking.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ARC_InternetBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient((typeof(IGenericService<,,>)), (typeof(GenericService<,,>)));

            services.AddTransient<IBeneficiarioService, BeneficiarioService>();
            services.AddTransient<ICuentaAhorroService, CuentaAhorroService>();
            services.AddTransient<ITarjetaCreditoService, TarjetaCreditoService>();
            services.AddTransient<ITransaccionService, TransaccionService>();
            services.AddTransient<IPrestamoService, PrestamoService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductoServices, ProductoServices>();
            services.AddTransient<IRetiroEfectivoService, RetiroEfectivoService>();
            
            #endregion
        }
    }
}
