using MediatR;

namespace Domain.Features.Matches.Get;

public record struct GetMatchQuery(Guid Id) : IRequest<GetMatchResult>;