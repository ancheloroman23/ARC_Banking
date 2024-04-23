using ARC_InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Services
{
    public interface ICuentaAhorroService : IGenericService<SaveCuentaAhorroViewModel, CuentaAhorroViewModel, CuentaAhorro>
    {
        Task AddAmountToPrincipalCuentaAhorro(SaveCuentaAhorroViewModel vm);
        Task<bool> AddAmountToCuentaAhorro(string userName, decimal amount);
        Task<List<CuentaAhorroViewModel>> GetAllVMbyUserId();
        Task<CuentaAhorroViewModel> GetByAccountINumber(string NumeroProducto);
        Task<SaveCuentaAhorroViewModel> GetVmByAccountNumber(string NumeroProducto);
    }
}
