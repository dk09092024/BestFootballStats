using MediatR;

namespace Domain.Features.Teams.LinkPlayer;

public record struct LinkPlayerToTeamRequest(Guid TeamId, Guid PlayerId) : IRequest;