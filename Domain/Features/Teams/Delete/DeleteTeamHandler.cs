using Domain.Repositories;
using MediatR;

namespace Domain.Features.Teams.Delete;

public class DeleteTeamHandler : IRequestHandler<DeleteTeamRequest>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamHandler(ITeamRepository teamRepository) => _teamRepository = teamRepository;

    public async Task Handle(DeleteTeamRequest request, CancellationToken cancellationToken)
    {
        await _teamRepository.DeleteAsync(request.Id,cancellationToken);
    }
}