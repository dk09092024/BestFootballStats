using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Leagues.Update;

public class UpdateLeagueValidator : AbstractValidator<UpdateLeagueRequest>
{
    public UpdateLeagueValidator(ILeagueRepository leagueRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Leagues ID is required.")
            .MustAsync(async (x,cancellationToken) => await leagueRepository.ExistsAsync(x, cancellationToken))
            .WithMessage("Leagues does not exist.");
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Leagues name is required.")
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Leagues name must be between 3 and 100 characters.");
    }
}