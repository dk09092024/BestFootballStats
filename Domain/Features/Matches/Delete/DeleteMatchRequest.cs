using MediatR;

namespace Domain.Features.Matches.Delete;

public record struct DeleteMatchRequest(Guid Id) : IRequest;