using Domain.Models.Common;

namespace Domain.Repositories.Common;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<bool> ExistsAsync(Guid id);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
}