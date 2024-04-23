using ARC_InternetBanking.Core.Application.Dtos.User;
using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro
{
    public class SaveCuentaAhorroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Colocar el usuario que le creara la cuenta")]
        public string? IdUsuario { get; set; }
        
        public string? NumeroProducto { get; set; }
        public bool EsPrincipal { get; set; } = false;

        [Range(1, double.MaxValue, ErrorMessage = "Colocar un cantidad mayor a cero(0)")]
        public decimal Cantidad { get; set; }

        public List<UserDto>? Users { get; set; }
    }
}