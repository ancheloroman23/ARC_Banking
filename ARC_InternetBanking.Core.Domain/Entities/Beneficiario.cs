using ARC_InternetBanking.Core.Domain.Common;

namespace ARC_InternetBanking.Core.Domain.Entities
{
    public class Beneficiario : AuditableBaseEntity
    {
        public string IdProducto { get; set; } = null!;
        public string BeneficiarioId { get; set; } = null!;
        public string UsuarioId { get; set; } = null!;
    }
}
