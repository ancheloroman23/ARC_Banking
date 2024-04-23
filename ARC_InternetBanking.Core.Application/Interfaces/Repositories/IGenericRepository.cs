

namespace ARC_InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task<Entity> GetByIdAsync(int id);
        Task<List<Entity>> GetAllAsync();
        Task UpdateAsync(Entity entity, int id);
        Task DeleteAsync(int id);
        Task<List<Entity>> GetAllWithIncludes(List<string> properties);
    }
}
