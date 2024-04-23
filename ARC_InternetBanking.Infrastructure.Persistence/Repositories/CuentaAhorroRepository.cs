


using ARC_InternetBanking.Infrastructure.Persistence.Contexts;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Domain.Entities;
using ARC_InternetBanking.Core.Application.Dictionary;

namespace ARC_InternetBanking.Infrastructure.Persistence.Repositories
{
    public class CuentaAhorroRepository : GenericRepository<CuentaAhorro>, ICuentaAhorroRepository
    {
        public CuentaAhorroRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public override async Task<CuentaAhorro> AddAsync(CuentaAhorro entity)
        {
            var list = await GetAllAsync();

            entity.NumeroProducto = ProductNumberGenerator.AlgorithmForProductNumber<CuentaAhorro>(ProductPrefixis.ProductPrefixDictionary["CuentaAhorro"], list);

            return await base.AddAsync(entity);
        }

        public async Task<CuentaAhorro> GetCuentaAhorroByOwner(string ownerUserName)
        {
            var allAccounts = await GetAllAsync();
            return allAccounts.FirstOrDefault(sa => sa.IdUsuario == ownerUserName);
        }


    }
}
