using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Colocar el Email")]
        [DataType(DataType.Text)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Colocar una Clave")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        #region Error

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion

    }
}
