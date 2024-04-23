using ARC_InternetBanking.Core.Domain.Common;

namespace ARC_InternetBanking.Core.Domain.Entities
{
    public class TipoTransaccion : AuditableBaseEntity
    {
        public string NombreTipoTransaccion { get; set; } = null!;

        public ICollection<Transaccion>? Transacciones { get; set; }
    }
}