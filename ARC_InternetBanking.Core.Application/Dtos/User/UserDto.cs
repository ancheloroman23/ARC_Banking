

namespace ARC_InternetBanking.Core.Application.Dtos.User
{
    public class UserDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UsuarioId { get; set; }
        public string Cedula { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
        public List<string>? Roles { get; set; }

        #region Errores

        public bool HasError { get; set; }
        public string? Error { get; set; }

        #endregion
    }
}
