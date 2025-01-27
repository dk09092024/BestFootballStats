using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Leagues.Get;

public abstract class GetLeagueValidator : AbstractValidator<GetLeagueQuery>
{
    public GetLeagueValidator(ILeagueRepository leagueRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("League ID is required.")
            .MustAsync(async (x,cancellationToken)=> await leagueRepository.ExistsAsync(x))
            .WithMessage("League does not exist.");
    }
}