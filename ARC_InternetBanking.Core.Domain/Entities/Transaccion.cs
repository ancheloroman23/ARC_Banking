using ARC_InternetBanking.Core.Domain.Common;

namespace ARC_InternetBanking.Core.Domain.Entities
{
    public class Transaccion : AuditableBaseEntity
    {
        public string OriginNumeroCuenta { get; set; } = null!;
        public string DestinoNumeroCuenta { get; set; } = null!;
        public decimal Cantidad { get; set; }
        public string IdUsuarioTitularCuenta { get; set; } = null!;        
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Today;

        public int IdTipoTransaccion { get; set; }
        public TipoTransaccion TipoTransaccion { get; set; } = null!;
    }
}