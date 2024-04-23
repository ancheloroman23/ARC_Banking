

using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ITarjetaCreditoRepository : IGenericRepository<TarjetaCredito>
    {
        Task<TarjetaCredito> GetByCardNumber(string cardNumber);
    }
}
