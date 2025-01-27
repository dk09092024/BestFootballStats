using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Matches.Add;

public class AddMatchValidator : AbstractValidator<AddMatchRequest>
{
    public AddMatchValidator(ITeamRepository teamRepository)
    {
        RuleFor(x => x.HomeTeamId)
            .NotEmpty()
            .WithMessage("Home team ID is required.")
            .MustAsync(async (x, cancellationToken) => await teamRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Home team does not exist.");
        RuleFor(x => x.AwayTeamId).NotEmpty()
            .WithMessage("Away team ID is required.")
            .MustAsync(async (x, cancellationToken) => await teamRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Away team does not exist.");
    }
}