

namespace ARC_InternetBanking.Core.Application.ViewModels.Prestamo
{
    public class PrestamoViewModel
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; } = null!;
        public string NumeroProducto { get; set; } = null!;
        public string UserName { get; set; }
        public decimal CantidaPrestamo { get; set; }
        public decimal CantidadPagada { get; set; }
    }
}