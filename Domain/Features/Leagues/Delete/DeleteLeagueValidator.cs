using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Leagues.Delete;

public class DeleteLeagueValidator : AbstractValidator<DeleteLeagueRequest>
{
    public DeleteLeagueValidator(ILeagueRepository leagueRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("League ID is required.")
            .MustAsync(async (x,cancellationToken)=> await leagueRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("League does not exist.");
    }
}