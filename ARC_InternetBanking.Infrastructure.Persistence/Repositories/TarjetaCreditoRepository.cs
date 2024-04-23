using Microsoft.EntityFrameworkCore;
using ARC_InternetBanking.Core.Application.Dictionary;
using ARC_InternetBanking.Core.Application.Helpers;
using ARC_InternetBanking.Core.Domain.Entities;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;
using ARC_InternetBanking.Infrastructure.Persistence.Contexts;


namespace ARC_InternetBanking.Infrastructure.Persistence.Repositories
{
    public class TarjetaCreditoRepository : GenericRepository<TarjetaCredito>, ITarjetaCreditoRepository
    {
        private readonly ApplicationContext _dbContext;
        public TarjetaCreditoRepository(ApplicationContext dbContext) : base(dbContext)
        {
           _dbContext = dbContext;
        }

        public override async Task<TarjetaCredito> AddAsync(TarjetaCredito entity)
        {
            var list = await GetAllAsync();

            entity.NumeroProducto = ProductNumberGenerator.AlgorithmForProductNumber<TarjetaCredito>(ProductPrefixis.ProductPrefixDictionary["TarjetaCreditos"], list);

            return await base.AddAsync(entity);
        }

        public async Task<TarjetaCredito> GetByCardNumber(string cardNumber)
        {
            return await _dbContext.TarjetaCreditos.FirstOrDefaultAsync(x => x.NumeroProducto == cardNumber);
        }
    }
}
