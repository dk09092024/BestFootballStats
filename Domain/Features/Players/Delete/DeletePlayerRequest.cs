using MediatR;

namespace Domain.Features.Players.Delete;

public record DeletePlayerRequest(Guid Id) : IRequest;