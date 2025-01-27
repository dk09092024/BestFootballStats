using Domain.Repositories;
using MediatR;

namespace Domain.Features.Leagues.Update;

public class UpdateLeagueHandler : IRequestHandler<UpdateLeagueRequest>
{
    private ILeagueRepository _leagueRepository;

    public UpdateLeagueHandler(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository;
    }

    public async Task Handle(UpdateLeagueRequest request, CancellationToken cancellationToken)
    {
        var league = await _leagueRepository.GetByIdAsync(request.Id,cancellationToken);
        league.Name = request.Name;
        await _leagueRepository.UpdateAsync(league,cancellationToken);
    }
}