using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Leagues.LinkTeam;

public class LinkTeamToLeagueValidator : AbstractValidator<LinkTeamToLeagueRequest>
{
    public LinkTeamToLeagueValidator(ILeagueRepository leagueRepository, ITeamRepository teamRepository)
    {
        RuleFor(x => x.LeagueId)
            .NotEmpty()
            .WithMessage("League ID is required.")
            .MustAsync(async (x, cancellationToken) => await leagueRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("League does not exist.");
        RuleFor(x => x.TeamId)
            .NotEmpty()
            .WithMessage("Teams ID is required.")
            .MustAsync(async (x, cancellationToken) => await teamRepository.ExistsAsync(x,cancellationToken))
            .WithMessage("Teams does not exist.");
    }
}