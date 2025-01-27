using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
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

    public async Task<League> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
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
        _context.Leagues.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        await _context.Leagues.Where(x=> x.Id==id).ExecuteDeleteAsync(cancellationToken);
    }
}