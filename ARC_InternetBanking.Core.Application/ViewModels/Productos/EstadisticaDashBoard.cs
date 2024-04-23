

namespace ARC_InternetBanking.Core.Application.ViewModels.Productos
{
    public class EstadisticaDashBoard
    {
        public int TotalTransacciones { get; set; }
        public int TransaccionDiarias { get; set; }
        public int TotalPagos { get; set; }
        public int PagoDiarios { get; set; }
        public int TotalClientesActivos { get; set; }
        public int TotalClientesInactivos { get; set; }
        public int TarjetasCreditoTotal { get; set; }
        public int CuentasAhorroTotal { get; set; }
        public int PrestamoTotal { get; set; }
        public int ProductosTotal { get; set; }
    }
}