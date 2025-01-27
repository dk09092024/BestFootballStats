using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Teams.Delete;

public class DeleteTeamValidator : AbstractValidator<DeleteTeamRequest>
{
    public DeleteTeamValidator(ITeamRepository teamRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync((id, cancellationToken) => teamRepository.ExistsAsync(id,cancellationToken))
            .WithMessage("Team not found.");
    }
}