using MediatR;

namespace Domain.Features.Matches.ComputeStatistics;

public record struct ComputeStatisticsRequest(Guid MatchId) :IRequest;