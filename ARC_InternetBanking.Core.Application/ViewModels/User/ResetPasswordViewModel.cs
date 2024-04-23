using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Colocar el Email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }        

        [Required(ErrorMessage = "Colocar una clave")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "La clave no es igual")]
        [Required(ErrorMessage = "Colocar una clave")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe tener TOKEN")]
        [DataType(DataType.Text)]
        public string? Token { get; set; }

        #region Error

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion
    }
}
