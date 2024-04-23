

namespace ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito
{
    public class TarjetaCreditoViewModel
    {
        public int Id { get; set; }
        public decimal Limite { get; set; }
        public decimal CantidadActual { get; set; }
        public decimal Deuda { get; set; }
        public string NumeroProducto { get; set; } = null!;
        public string IdUsario { get; set; } = null!;
        public string UserName { get; set; }
    }
}