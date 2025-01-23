using MediatR;

namespace Domain.Features.Matches.Update;

public record struct UpdateMatchRequest(Guid MatchId, Guid HomeTeamId, Guid AwayTeamId) : IRequest;