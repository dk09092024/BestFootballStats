using Domain.Repositories;
using MediatR;

namespace Domain.Features.Teams.LinkPlayer;

public class LinkPlayerToTeamHandler : IRequestHandler<LinkPlayerToTeamRequest>
{
    private readonly IPlayerRepository _playerRepository;

    public LinkPlayerToTeamHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public Task Handle(LinkPlayerToTeamRequest request, CancellationToken cancellationToken)
    {
        return _playerRepository.LinkToTeam(request.PlayerId, request.TeamId, cancellationToken);
    }
}