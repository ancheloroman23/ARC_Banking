using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.User
{
    public class EditUserViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Colocar el nombre")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Colocar el apellido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Colocar un nombre de Usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        

        [Required(ErrorMessage = "Colocar un Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        public string Phone { get; set; }
        public List<string> Roles { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Colocar su cedula sin guiones(-)")]
        [Required(ErrorMessage = "Colocar su Cedula")]
        public string Cedula { get; set; } = null!;

        public decimal MontoInicial { get; set; }

        #region Error

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion
    }
}
