using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Matches.Update;

public class UpdateMatchValidator : AbstractValidator<UpdateMatchRequest>
{
    public UpdateMatchValidator(IMatchRepository matchRepository)
    {
        RuleFor(x => x.MatchId)
            .NotEmpty()
            .WithMessage("Match ID is required.")
            .MustAsync(async (x, cancellationToken) => await matchRepository.ExistsAsync(x))
            .WithMessage("Match does not exist.");
        RuleFor(x => x.HomeTeamId)
            .NotEmpty()
            .WithMessage("Home Team ID is required.")
            .MustAsync(async (x, cancellationToken) => await matchRepository.ExistsAsync(x))
            .WithMessage("Home Team does not exist.");
    }
}