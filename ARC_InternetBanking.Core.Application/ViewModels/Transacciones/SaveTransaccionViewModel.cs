using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.Transacciones
{
    public class SaveTransaccionViewModel
    {        
        [Range(1, double.MaxValue, ErrorMessage = "Cantidad debe ser mayor que 1")]
        public decimal Cantidad { get; set; }

        public string? IdUsuarioTitularCuenta { get; set; } = null!;
        public string? Descriptcion { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public int IdTipoTransaccion { get; set; }
        public string? Nombre { get; set; } = null!;
        public string? Apellido { get; set; } = null!;

        [Required(ErrorMessage = "Colocar cuenta origen")]
        [Range(1, double.MaxValue, ErrorMessage = "Colocar Numero de cuenta")]
        public string OriginNumeroCuenta { get; set; } = null!;

        [Required(ErrorMessage = "Colocar cuenta destino")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numero de cuenta debe tener 9 digitos")]
        public string DestinoNumeroCuenta { get; set; } = null!;

        #region Error

        public bool HasError { get; set; } = false;
        public string? ErrorMessage { get; set; }

        #endregion        
    }
}