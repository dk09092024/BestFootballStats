using MediatR;

namespace Domain.Features.Leagues.Delete;

public record struct DeleteLeagueRequest(Guid Id) : IRequest;