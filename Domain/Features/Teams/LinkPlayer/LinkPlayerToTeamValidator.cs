using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Teams.LinkPlayer;

public class LinkPlayerToTeamValidator : AbstractValidator<LinkPlayerToTeamRequest>
{
    public LinkPlayerToTeamValidator(IPlayerRepository playerRepository, ITeamRepository teamRepository)
    {
        RuleFor(x => x.PlayerId)
            .NotEmpty()
            .NotNull()
            .WithMessage("PlayerId is required.")
            .MustAsync((id, cancellationToken) => playerRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Player not found.");
        RuleFor(x => x.TeamId)
            .NotEmpty()
            .NotNull()
            .WithMessage("TeamId is required.")
            .MustAsync((id, cancellationToken) => teamRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Team not found.");
    }
}