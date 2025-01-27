using Domain.Repositories;
using MediatR;

namespace Domain.Features.Matches.Delete;

public class DeleteMatchHandler : IRequestHandler<DeleteMatchRequest>
{
    private readonly IMatchRepository _matchRepository;

    public DeleteMatchHandler(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task Handle(DeleteMatchRequest request, CancellationToken cancellationToken)
    {
        await _matchRepository.DeleteAsync(request.Id,cancellationToken);
    }
}