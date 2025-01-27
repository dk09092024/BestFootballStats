using System.Reflection.Metadata.Ecma335;
using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Matches.Add;

public class AddMatchHandler : IRequestHandler<AddMatchRequest, AddMatchResult>
{
    private readonly IMatchRepository _matchRepository;

    public async Task<AddMatchResult> Handle(AddMatchRequest request, CancellationToken cancellationToken)
    {
        var match = new Match
        {
            HomeTeamId = request.HomeTeamId,
            AwayTeamId = request.AwayTeamId,
            Id = default,
            CreatedAt = default
        };
        return new(await _matchRepository.AddAsync(match,cancellationToken));
    }
}