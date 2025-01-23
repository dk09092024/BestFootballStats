using Domain.Repositories;
using MediatR;

namespace Domain.Features.Matches.Get;

public class GetMatchHandler : IRequestHandler<GetMatchQuery, GetMatchResult>
{
    private readonly IMatchRepository _matchRepository;

    public GetMatchHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<GetMatchResult> Handle(GetMatchQuery query, CancellationToken cancellationToken)
    {
        var match = await _matchRepository.GetByIdAsync(query.Id);
        return new GetMatchResult(match);
    }
}