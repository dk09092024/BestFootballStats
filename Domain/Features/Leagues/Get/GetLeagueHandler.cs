using Domain.Repositories;
using MediatR;

namespace Domain.Features.Leagues.Get;

public class GetLeagueHandler : IRequestHandler<GetLeagueQuery, GetLeagueResult>
{
    private ILeagueRepository _leagueRepository;

    public GetLeagueHandler(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository;
    }

    public async Task<GetLeagueResult> Handle(GetLeagueQuery query, CancellationToken cancellationToken)
    {
        var league = await _leagueRepository.GetByIdAsync(query.Id,cancellationToken);
        return new GetLeagueResult(league);
    }
}