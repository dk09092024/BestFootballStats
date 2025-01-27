using Domain.Models.Common;

namespace Domain.Repositories.Common;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<bool> ExistsAsync(Guid id,CancellationToken cancellationToken);
    Task<TEntity> GetByIdAsync(Guid id,CancellationToken cancellationToken);
    Task<Guid> AddAsync(TEntity entity,CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity,CancellationToken cancellationToken);
    Task DeleteAsync(Guid id,CancellationToken cancellationToken);
}