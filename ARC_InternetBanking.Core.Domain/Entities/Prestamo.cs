

using ARC_InternetBanking.Core.Domain.Common;

namespace ARC_InternetBanking.Core.Domain.Entities
{
    public class Prestamo : Producto
    {
        public decimal CantidaPrestamo { get; set; }
        public decimal CantidadPagada { get; set; }
    }
}
