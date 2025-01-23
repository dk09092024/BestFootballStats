using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Players.Update;

public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerRepository>
{
    private readonly IPlayerRepository _playerRepository;

    public UpdatePlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task Handle(UpdatePlayerRepository request, CancellationToken cancellationToken)
    {
        var player = new Player
        {
            Id = request.Id,
            Name = request.Name,
            Position = request.Position,
            TeamId = default,
            CreatedAt = default
        };
        await _playerRepository.UpdateAsync(player);
    }
}