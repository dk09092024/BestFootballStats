using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MatchRepository : IMatchRepository
{
    private readonly ApplicationContext _context;

    public MatchRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Matches.AnyAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<Match> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Matches.FirstAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<Guid> AddAsync(Match entity,CancellationToken cancellationToken)
    {
        await _context.Matches.AddAsync(entity,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(Match entity, CancellationToken cancellationToken)
    {
        _context.Matches.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        await _context.Matches.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    }
}