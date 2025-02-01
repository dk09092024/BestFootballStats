using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly ApplicationContext _context;
    private ILeagueRepository _leagueRepository;

    public TeamRepository(ApplicationContext context, ILeagueRepository leagueRepository)
    {
        _context = context;
        _leagueRepository = leagueRepository;
    }

    public async Task<bool> ExistsAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Teams.AnyAsync(x => x.Id == id,cancellationToken);
    }
    public async Task MustExistAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await ExistsAsync(id, cancellationToken))
        {
            throw new EntityDoesNotExistExeption<TeamRepository, Team>(id);
        }
    }

    public async Task<Team> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        await MustExistAsync(id,cancellationToken);
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
        await MustExistAsync(entity.Id,cancellationToken);
        _context.Teams.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        _context.Teams.Remove(await GetByIdAsync(id,cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task LinkToLeague(Guid teamId, Guid leagueId,CancellationToken cancellationToken)
    {
        await MustExistAsync(teamId, cancellationToken);
        await _leagueRepository.MustExistAsync(leagueId, cancellationToken);
        await _context.Teams.Where(x=> x.Id==teamId).ExecuteUpdateAsync(
            x => x.SetProperty(x=> x.LeagueId, leagueId), cancellationToken);
    }
}