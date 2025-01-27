using Domain.Repositories;
using MediatR;

namespace Domain.Features.Teams.Get;

public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, GetTeamResponse>
{
    private readonly ITeamRepository _teamRepository;

    public GetTeamQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<GetTeamResponse> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetTeamResponse(team);
    }
}