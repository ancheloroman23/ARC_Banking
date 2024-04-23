
using ARC_InternetBanking.Infrastructure.Persistence.Contexts;
using ARC_InternetBanking.Core.Domain.Entities;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;

namespace ARC_InternetBanking.Infrastructure.Persistence.Repositories
{
    public class TransaccionRepository : GenericRepository<Transaccion>, ITransaccionRepository
    {
        public TransaccionRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
