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
            .MustAsync((id, token) => teamRepository.ExistsAsync(id))
            .WithMessage("Team not found.");
    }
}