using System.ComponentModel.DataAnnotations;

namespace ARC_InternetBanking.Core.Application.ViewModels.User
{
    public class SaveUserViewModel
    {
        [Required(ErrorMessage = "Colocar el nombre")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Colocar el apellido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "La clave para debe tener al menos un numero y un caracteres especiales")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "La clave no coiciden")]
        [Required(ErrorMessage = "Colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Colocar su cedula sin guiones(-)")]
        [Required(ErrorMessage = "Colocar su Cedula")]
        public string Cedula { get; set; } = null!;

        public decimal MontoInicial { get; set; }

        public bool IsAdmin { get; set; }               
    }
}
