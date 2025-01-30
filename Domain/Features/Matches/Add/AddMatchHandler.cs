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
    private Mediator _mediator;

    public AddMatchHandler(Mediator mediator, IMatchRepository matchRepository)
    {
        _mediator = mediator;
        _matchRepository = matchRepository;
    }

    public AddMatchHandler(IMatchRepository matchRepository)
    {
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
        return new AddMatchResult(await _matchRepository.AddAsync(match, cancellationToken));
    }
}