using ARC_InternetBanking.Core.Domain.Common;

namespace ARC_InternetBanking.Core.Domain.Entities
{
    public class CuentaAhorro : Producto
    {
        public bool EsPrincipal { get; set; } = false;
        public decimal Cantidad { get; set; }
    }
}
