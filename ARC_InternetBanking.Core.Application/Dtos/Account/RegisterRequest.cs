namespace ARC_InternetBanking.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Cedula { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
