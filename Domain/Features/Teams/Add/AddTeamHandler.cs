using Domain.Models;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Teams.Add;

public class AddTeamHandler : IRequestHandler<AddTeamRequest, AddTeamResponse>
{
    private readonly ITeamRepository _teamRepository;

    public AddTeamHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<AddTeamResponse> Handle(AddTeamRequest request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            Name = request.Name,
            Id = default,
            CreatedAt = default
        };
        await _teamRepository.AddAsync(team,cancellationToken);
        return new AddTeamResponse(team.Id);
    }
}