
using ARC_InternetBanking.Core.Domain.Entities;

namespace ARC_InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ICuentaAhorroRepository : IGenericRepository<CuentaAhorro>
    {
        Task<CuentaAhorro> GetCuentaAhorroByOwner(string ownerUserName);
    }
}
