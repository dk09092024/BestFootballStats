using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Leagues.Delete;

public abstract class DeleteLeagueRequestValidator : AbstractValidator<DeleteLeagueRequest>
{
    public DeleteLeagueRequestValidator(ILeagueRepository leagueRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("League ID is required.")
            .MustAsync(async (x,cancellationToken)=> await leagueRepository.ExistsAsync(x))
            .WithMessage("League does not exist.");
    }
}