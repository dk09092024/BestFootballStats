using MediatR;

namespace Domain.Features.Teams.Get;

public record struct GetTeamQuery(Guid Id) : IRequest<GetTeamResponse>;