using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Matches.ComputeStatistics;

public class ComputeStatisticsHandler : IRequestHandler<ComputeStatisticsRequest>
{
    private readonly IMatchRepository _matchRepository;

    public ComputeStatisticsHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task Handle(ComputeStatisticsRequest request, CancellationToken cancellationToken)
    {
        var match = await _matchRepository.GetByIdAsync(request.MatchId, cancellationToken);
        CalculatePassingStatistics(match);
        await _matchRepository.UpdateAsync(match, cancellationToken);
    }

    private void CalculatePassingStatistics(Match match)
    {
        match.TotalPasses = new Random().Next(100, 1000);
    }
}