using Domain.Repositories;
using MediatR;

namespace Domain.Features.Teams.Update;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamRequest>
{
    private readonly ITeamRepository _teamRepository;

    public UpdateTeamHandler(ITeamRepository teamRepository) => _teamRepository = teamRepository;

    public async Task Handle(UpdateTeamRequest request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        team.Name = request.Name;
        await _teamRepository.UpdateAsync(team, cancellationToken);
    }
}