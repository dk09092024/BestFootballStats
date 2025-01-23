using MediatR;

namespace Domain.Features.Leagues.LinkTeam;

public record struct LinkTeamToLeagueRequest(Guid LeagueId, Guid TeamId) : IRequest;