using Domain.Models.Common;
using Domain.Repositories.Common;

namespace Infrastructure.Repositories.Exceptions;

public class EntityDoesNotExistExeption<TRepository,TEntity> : Exception where TRepository : IRepository<TEntity> where TEntity : Entity
{
    public EntityDoesNotExistExeption(Guid id) : base($"{typeof(TEntity).Name} with id {id} not exists in {typeof(TRepository).Name}")
    {
    }
    
}