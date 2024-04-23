

namespace ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro
{
    public class UpdateCuentaAhorroViewModel
    {
        public string NombreProducto { get; set; } = null!;
        public decimal CantidadToAdd { get; set; }
        public bool IsPrincipal { get; set; }        
    }
}
