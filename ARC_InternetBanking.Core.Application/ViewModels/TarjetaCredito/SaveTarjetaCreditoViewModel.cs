using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.ViewModels.Transacciones;
using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito
{
    public class SaveTarjetaCreditoViewModel
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "Colocar una cantidad mayor que 1")]
        public decimal Limite { get; set; }

        public decimal CantidadActual { get; set; }
        public decimal Deuda { get; set; }

        [Required(ErrorMessage = "Colocar el usuario que creara la cuenta")]
        [Range(1, double.MaxValue, ErrorMessage = "Colocar Numero de Cuenta")]
        public string NumeroProducto { get; set; } = null!;

        public string? UserName { get; set; }

        public SaveTransaccionViewModel? SaveTransaccionViewModel { get; set; }

        public List<UserDto>? users;
    }
}