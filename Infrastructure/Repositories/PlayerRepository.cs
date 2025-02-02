using Domain.Models;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationContext _context;
    private ITeamRepository _teamRepository;

    public PlayerRepository(ApplicationContext context, ITeamRepository teamRepository)
    {
        _context = context;
        _teamRepository = teamRepository;
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Players.AnyAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task MustExistAsync(Guid id, CancellationToken cancellationToken)
    {
        if (!await ExistsAsync(id, cancellationToken))
        {
            throw new EntityDoesNotExistExeption<PlayerRepository, Player>(id);
        }
    }

    public async Task<Player> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        await MustExistAsync(id,cancellationToken);
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
        await MustExistAsync(entity.Id,cancellationToken);
        _context.Players.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id,CancellationToken cancellationToken)
    {
        _context.Players.Remove(await GetByIdAsync(id,cancellationToken));
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task LinkToTeam(Guid playerId, Guid teamId,CancellationToken cancellationToken)
    {
        await MustExistAsync(playerId,cancellationToken);
        await _teamRepository.MustExistAsync(teamId,cancellationToken);
        
        var player = await GetByIdAsync(playerId,cancellationToken);
        player.TeamId = teamId;
        await UpdateAsync(player,cancellationToken);
    }
}