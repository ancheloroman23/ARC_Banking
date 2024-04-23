using ARC_InternetBanking.Core.Application.Dtos.User;
using ARC_InternetBanking.Core.Application.ViewModels.Transacciones;
using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.Prestamo
{
    public class SavePrestamoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Colocar usuario que creara la cuenta")]
        [Range(1, double.MaxValue, ErrorMessage = "Colocar numero de Cuenta")]
        public string IdUsuario { get; set; } = null!;

        public string NumeroProducto { get; set; } = null!;
        public string? UserName { get; set; } = null!;

        [Range(1, double.MaxValue, ErrorMessage = "Cantidad debe ser mayor que 1")]
        public decimal CantidaPrestamo { get; set; }
        public decimal CantidadPagada { get; set; }

        public List<UserDto>? users;

        [Required(ErrorMessage = "Requerido")]
        public SaveTransaccionViewModel? SaveTransaccionViewModel { get; set; }

        public bool? Status { get; set; } = true;
    }
}