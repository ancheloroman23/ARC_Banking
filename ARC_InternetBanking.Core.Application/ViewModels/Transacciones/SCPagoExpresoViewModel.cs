

namespace ARC_InternetBanking.Core.Application.ViewModels.Transacciones
{
    public class SCPagoExpresoViewModel
    {
        public string? Nombre { get; set; } = null!;
        public string? Apellido { get; set; } = null!;
        public SaveTransaccionViewModel? SaveTransaccionViewModel { get; set; }
    }
}
