using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Players.Update;

public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerRequest>
{
    private readonly IPlayerRepository _playerRepository;

    public UpdatePlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task Handle(UpdatePlayerRequest request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(request.Id,cancellationToken);
        player.Name = request.Name;
        player.Position = request.Position;
        await _playerRepository.UpdateAsync(player,cancellationToken);
    }
}