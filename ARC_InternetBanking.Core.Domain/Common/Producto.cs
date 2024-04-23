
namespace ARC_InternetBanking.Core.Domain.Common
{
    public class Producto : AuditableBaseEntity
    {
        public string IdUsuario { get; set; } = null!;
        public string NumeroProducto { get; set; } = null!;
    }
}
