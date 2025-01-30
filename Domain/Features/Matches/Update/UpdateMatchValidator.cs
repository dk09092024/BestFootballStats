using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Matches.Update;

public class UpdateMatchValidator : AbstractValidator<UpdateMatchRequest>
{
    public UpdateMatchValidator(IMatchRepository matchRepository, ITeamRepository teamRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Match ID is required.")
            .MustAsync(async (x, cancellationToken) => await matchRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Match does not exist.");
        RuleFor(x => x.HomeTeamId)
            .NotEmpty()
            .WithMessage("Home Team ID is required.")
            .MustAsync(async (x, cancellationToken) => await teamRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Home Team does not exist.");
        RuleFor(x => x.AwayTeamId)
            .NotEmpty()
            .WithMessage("Away Team ID is required.")
            .MustAsync(async (x, cancellationToken) => await teamRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Away Team does not exist.");
    }
}