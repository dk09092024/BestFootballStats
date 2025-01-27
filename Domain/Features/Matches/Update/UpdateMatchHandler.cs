using Domain.Repositories;
using MediatR;

namespace Domain.Features.Matches.Update;

public class UpdateMatchHandler : IRequestHandler<UpdateMatchRequest>
{
    private readonly IMatchRepository _matchRepository;

    public UpdateMatchHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task Handle(UpdateMatchRequest request, CancellationToken cancellationToken)
    {
        var match = await _matchRepository.GetByIdAsync(request.MatchId,cancellationToken);
        match.HomeTeamId = request.HomeTeamId;
        match.AwayTeamId = request.AwayTeamId;
        await _matchRepository.UpdateAsync(match,cancellationToken);
    }
}