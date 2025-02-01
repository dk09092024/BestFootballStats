using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LeagueRepository : ILeagueRepository
{
    private readonly ApplicationContext _context;

    public LeagueRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Leagues.AnyAsync(x => x.Id == id,cancellationToken);
    }

    public async Task MustExistAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await ExistsAsync(id, cancellationToken))
        {
            throw new EntityDoesNotExistExeption<LeagueRepository, League>(id);
        }
    }

    public async Task<League> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        if (!await ExistsAsync(id,cancellationToken))
        {
            throw new EntityDoesNotExistExeption<LeagueRepository,League>(id);
        }
        return await _context.Leagues.FirstAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<Guid> AddAsync(League entity,CancellationToken cancellationToken)
    {
        await _context.Leagues.AddAsync(entity,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(League entity,CancellationToken cancellationToken)
    {
        if (!await ExistsAsync(entity.Id,cancellationToken))
        {
            throw new EntityDoesNotExistExeption<LeagueRepository,League>(entity.Id);
        }
        _context.Leagues.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        _context.Leagues.Remove(await GetByIdAsync(id,cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
    }
}