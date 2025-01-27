using MediatR;

namespace Domain.Features.Players.Get;

public record struct GetPlayerQuery(Guid Id) : IRequest<GetPlayerResult>;