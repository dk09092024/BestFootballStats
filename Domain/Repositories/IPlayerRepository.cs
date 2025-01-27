using Domain.Models;
using Domain.Repositories.Common;

namespace Domain.Repositories;

public interface IPlayerRepository : IRepository<Player>
{
    Task LinkToTeam(Guid payerId, Guid teamId,CancellationToken cancellationToken);
}