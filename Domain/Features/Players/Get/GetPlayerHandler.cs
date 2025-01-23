using Domain.Repositories;
using MediatR;

namespace Domain.Features.Players.Get;

public class GetPlayerHandler : IRequestHandler<GetPlayerQuery, GetPlayerResult>
{
    private readonly IPlayerRepository _playerRepository;

    public GetPlayerHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<GetPlayerResult> Handle(GetPlayerQuery query, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.GetByIdAsync(query.Id);
        return new GetPlayerResult(player);
    }
}