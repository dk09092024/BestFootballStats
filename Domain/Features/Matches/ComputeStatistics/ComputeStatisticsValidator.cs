using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Matches.ComputeStatistics;

public class ComputeStatisticsValidator : AbstractValidator<ComputeStatisticsRequest>
{
    public ComputeStatisticsValidator(IMatchRepository matchRepository)
    {
        RuleFor(x => x.MatchId)
            .NotEmpty()
            .NotNull()
            .WithMessage("MatchId is required")
            .MustAsync(async (id, cancellationToken) => await matchRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Match does not exist");
    }
}