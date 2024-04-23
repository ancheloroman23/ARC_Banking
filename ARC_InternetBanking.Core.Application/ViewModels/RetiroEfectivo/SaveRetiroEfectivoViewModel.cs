using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.RetiroEfectivo
{
    public class SaveRetiroEfectivoViewModel
    {
        public string? UserId { get; set; }

        [Required(ErrorMessage = "Colocar la cuenta ahorro para hacer el avance de efectivo")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar el numero de cuaenta")]
        public string DestinoCuenta { get; set; }

        [Required(ErrorMessage = "Colocar Tarjeta de credito donde pagara")]
        [Range(1, double.MaxValue, ErrorMessage = "Seleccionar el numero de cuaenta")]
        public string NumeroTarjeta { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Colocar cantidad mayor que cero(0)")]
        public decimal Cantidad { get; set; }

        #region Error

        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }

        #endregion
    }
}