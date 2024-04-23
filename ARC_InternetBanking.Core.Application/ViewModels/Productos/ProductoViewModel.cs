using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Application.ViewModels.Prestamo;
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;

namespace ARC_InternetBanking.Core.Application.ViewModels.Productos
{
    public class ProductoViewModel
    {
        public List<CuentaAhorroViewModel>? CuentaAhorros { get; set; }
        public List<PrestamoViewModel>? Prestamos { get; set; }
        public List<TarjetaCreditoViewModel>? TarjetaCreditos { get; set; }
    }
}
