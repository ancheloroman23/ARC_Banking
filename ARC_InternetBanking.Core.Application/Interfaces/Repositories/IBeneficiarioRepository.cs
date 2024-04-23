

using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IBeneficiarioRepository : IGenericRepository<Beneficiario>
    {
        Task<bool> AddBeneficiario(string NumeroProducto);
        Task<List<Beneficiario>> GetBeneficiryByUserId(string idUser);
    }
}
