using Domain.Models;
using Domain.Repositories.Common;

namespace Domain.Repositories;

public interface ITeamRepository : IRepository<Team>
{
    Task LinkToLeague(Guid teamId, Guid leagueId,CancellationToken cancellationToken);
}