using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationContext _context;

    public TeamRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Teams.AnyAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<Team> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Teams.FirstAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<Guid> AddAsync(Team entity,CancellationToken cancellationToken)
    {
        await _context.Teams.AddAsync(entity,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(Team entity,CancellationToken cancellationToken)
    {
        _context.Teams.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        await _context.Teams.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task LinkToLeague(Guid teamId, Guid leagueId,CancellationToken cancellationToken)
    {
        await _context.Teams.Where(x=> x.Id==teamId).ExecuteUpdateAsync(
            x => x.SetProperty(x=> x.LeagueId, leagueId), cancellationToken);
    }
}