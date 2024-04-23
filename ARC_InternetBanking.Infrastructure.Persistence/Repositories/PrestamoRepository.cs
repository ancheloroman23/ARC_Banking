using ARC_InternetBanking.Core.Application.Dictionary;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Core.Domain.Entities;
using ARC_InternetBanking.Infrastructure.Persistence.Contexts;

namespace ARC_InternetBanking.Infrastructure.Persistence.Repositories
{
    public class PrestamoRepository : GenericRepository<Prestamo>, IPrestamoRepository
    {
        public PrestamoRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Prestamo> AddAsync(Prestamo entity)
        {
            var list = await GetAllAsync();

            entity.NumeroProducto = ProductNumberGenerator.AlgorithmForProductNumber<Prestamo>(ProductPrefixis.ProductPrefixDictionary["Transaccions"], list);

            return await base.AddAsync(entity);
        }

    }
}
