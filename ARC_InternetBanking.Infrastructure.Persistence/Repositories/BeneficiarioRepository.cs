
using Microsoft.EntityFrameworkCore;
using ARC_InternetBanking.Core.Domain.Entities;
using ARC_InternetBanking.Infrastructure.Persistence.Contexts;
using ARC_InternetBanking.Core.Application.Interfaces.Repositories;

namespace ARC_InternetBanking.Infrastructure.Persistence.Repositories
{
    public class BeneficiarioRepository : GenericRepository<Beneficiario>, IBeneficiarioRepository
    {
        private readonly ApplicationContext _dbContext;
        public BeneficiarioRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddBeneficiario(string NumeroProducto)
        {
            return true;            
        }

        public async Task<List<Beneficiario>> GetBeneficiryByUserId(string idUser)
        {
            return await _dbContext.Beneficiarios.Where(x => x.UsuarioId == idUser).ToListAsync();
        }

    }
}
