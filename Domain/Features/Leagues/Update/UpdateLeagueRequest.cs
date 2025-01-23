using MediatR;

namespace Domain.Features.Leagues.Update;

public record struct UpdateLeagueRequest(Guid Id,string Name) : IRequest;