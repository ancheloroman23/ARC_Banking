

using ARC_InternetBanking.Core.Application.ViewModels.Beneficiarios;
using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiarioService : IGenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario>
    {
        Task<bool> AddBeneficiario(string NumeroProducto);
        Task<List<BeneficiarioViewModel>> GetBeneficiryByUserId();
    }
}
