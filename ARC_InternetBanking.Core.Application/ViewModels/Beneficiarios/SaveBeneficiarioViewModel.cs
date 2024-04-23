using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.Beneficiarios
{
    public class SaveBeneficiarioViewModel
    {
        [Required(ErrorMessage = "Colocar el numero de cuenta")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El numero de cuenta debe tener 9 digto")]
        public string IdProducto { get; set; } = null!;
        public string? UserNameBeneficiario { get; set; } = null!;
        public string? UserName { get; set; } = null!;

        #region Error

        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }

        #endregion
    }
}
