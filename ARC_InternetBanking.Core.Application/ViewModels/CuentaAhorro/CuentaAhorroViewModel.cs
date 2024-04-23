
namespace ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro
{
    public class CuentaAhorroViewModel
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public string UserName { get; set; }
        public string NumeroProducto { get; set; }
        public decimal Cantidad { get; set; }
        public bool EsPrincipal { get; set; }
    }
}