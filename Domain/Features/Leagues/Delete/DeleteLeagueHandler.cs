using Domain.Repositories;
using MediatR;

namespace Domain.Features.Leagues.Delete;

public class DeleteLeagueHandler : IRequestHandler<DeleteLeagueRequest>
{
    private ILeagueRepository _leagueRepository;

    public DeleteLeagueHandler(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository;
    }


    public async Task Handle(DeleteLeagueRequest request, CancellationToken cancellationToken)
    {
        await _leagueRepository.DeleteAsync(request.Id,cancellationToken);
    }
}