using MediatR;

namespace Domain.Features.Leagues.Add;

public record struct AddLeagueRequest(string Name) : IRequest<AddLeagueResult>;