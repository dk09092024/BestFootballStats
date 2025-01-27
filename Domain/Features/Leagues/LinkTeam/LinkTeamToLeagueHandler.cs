using Domain.Repositories;
using MediatR;

namespace Domain.Features.Leagues.LinkTeam;

public class LinkTeamToLeagueHandler : IRequestHandler<LinkTeamToLeagueRequest>
{
    private readonly ILeagueRepository _leagueRepository;
    private readonly ITeamRepository _teamRepository;

    public LinkTeamToLeagueHandler(ILeagueRepository leagueRepository, ITeamRepository teamRepository)
    {
        _leagueRepository = leagueRepository;
        _teamRepository = teamRepository;
    }

    public async Task Handle(LinkTeamToLeagueRequest request, CancellationToken cancellationToken)
    {
        await _teamRepository.LinkToLeague(request.TeamId,request.LeagueId);
    }
}