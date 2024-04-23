

namespace ARC_InternetBanking.Core.Application.ViewModels.Beneficiarios
{
    public class BeneficiarioViewModel
    {
        public string IdProducto { get; set; } = null!;
        public string UserNameBeneficiario { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;

        public string? ErrorMessage { get; set; }
    }
}