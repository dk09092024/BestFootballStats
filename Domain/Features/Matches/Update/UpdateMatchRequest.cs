using MediatR;

namespace Domain.Features.Matches.Update;

public record struct UpdateMatchRequest(Guid Id, Guid HomeTeamId, Guid AwayTeamId) : IRequest;