using MediatR;

namespace Domain.Features.Leagues.Get;

public record struct GetLeagueQuery(Guid Id) : IRequest<GetLeagueResult>;