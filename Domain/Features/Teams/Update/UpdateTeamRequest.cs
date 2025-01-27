using MediatR;

namespace Domain.Features.Teams.Update;

public record struct UpdateTeamRequest(Guid Id, string Name) : IRequest;