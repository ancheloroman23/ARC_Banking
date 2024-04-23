using ARC_InternetBanking.Core.Domain.Common;

namespace ARC_InternetBanking.Core.Domain.Entities
{
    public class TarjetaCredito : Producto
    { 
        public decimal Limite { get; set; }
        public decimal CantidadActual { get; set; }
        public decimal Deuda { get; set; }
    }
}