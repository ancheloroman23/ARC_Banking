
using ARC_InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITarjetaCreditoService : IGenericService<SaveTarjetaCreditoViewModel, TarjetaCreditoViewModel, TarjetaCredito>
    {
        Task<List<TarjetaCreditoViewModel>> GetAllVMbyUserId();

        Task<SaveTarjetaCreditoViewModel> GetByCardNumber(string cardNumber);
        Task<TarjetaCreditoViewModel> GetByCardIdentifyinNumber(string NumeroProducto);
    }
}
