using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationContext _context;

    public PlayerRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Players.AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Player> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Players.FirstAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<Guid> AddAsync(Player entity,CancellationToken cancellationToken)
    {
        await _context.Players.AddAsync(entity,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(Player entity,CancellationToken cancellationToken)
    {
        _context.Players.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        await _context.Players.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task LinkToTeam(Guid payerId, Guid teamId,CancellationToken cancellationToken)
    {
        await _context.Players.Where(x=> x.Id==payerId).ExecuteUpdateAsync(
            x => x.SetProperty(x=> x.TeamId, teamId), cancellationToken);
    }
}