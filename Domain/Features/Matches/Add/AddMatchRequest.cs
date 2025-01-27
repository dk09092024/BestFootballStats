using MediatR;

namespace Domain.Features.Matches.Add;

public record struct AddMatchRequest(Guid HomeTeamId, Guid AwayTeamId) : IRequest<AddMatchResult>;