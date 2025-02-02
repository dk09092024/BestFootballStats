using Domain.Models;
using Domain.Models.Enum;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Players.Add;

public class AddPlayerHandler : IRequestHandler<AddPlayerRequest, AddPlayerResult>
{
    private readonly IPlayerRepository _playerRepository;

    public AddPlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<AddPlayerResult> Handle(AddPlayerRequest request, CancellationToken cancellationToken)
    {
        var player = new Player
        {
            Name = request.Name,
            Position = (Position)request.Position,
            Id = default,
            CreatedAt = default
        };
        await _playerRepository.AddAsync(player,cancellationToken);
        return new AddPlayerResult(player.Id);
    }
}