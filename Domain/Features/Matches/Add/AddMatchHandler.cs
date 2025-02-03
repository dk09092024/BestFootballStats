using System.Reflection.Metadata.Ecma335;
using Domain.Features.Matches.ComputeStatistics;
using Domain.Models;
using Domain.Repositories;
using Hangfire;
using MediatR;

namespace Domain.Features.Matches.Add;

public class AddMatchHandler : IRequestHandler<AddMatchRequest, AddMatchResult>
{
    private readonly IMatchRepository _matchRepository;
    private IMediator _mediator;

    public AddMatchHandler(IMediator mediator, IMatchRepository matchRepository)
    {
        _mediator = mediator;
        _matchRepository = matchRepository;
    }
    
    public async Task<AddMatchResult> Handle(AddMatchRequest request, CancellationToken cancellationToken)
    {
        var match = new Match
        {
            HomeTeamId = request.HomeTeamId,
            AwayTeamId = request.AwayTeamId,
            Id = default,
            CreatedAt = default
        };
        var matchId = await _matchRepository.AddAsync(match, cancellationToken);
        StartCalculationForMatchStatisticsInHangfire(matchId);
        return new AddMatchResult
        {
            Id = matchId
        };
    }
    
    public void StartCalculationForMatchStatisticsInHangfire(Guid matchId)
    {
        BackgroundJob.Enqueue(() => CalculateMatchStatistics(matchId));
    }
    public void CalculateMatchStatistics(Guid matchId)
    {
        _mediator.Send(new ComputeStatisticsRequest
        {
            MatchId = matchId
        });
    }
}