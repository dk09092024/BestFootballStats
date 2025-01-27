using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Teams.Update;

public class UpdateTeamValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamValidator(ITeamRepository teamRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync((id, cancellationToken) => teamRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Team not found.");
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(3)
            .MaximumLength(100)
            .WithMessage("Name must be between 3 and 100 characters.");
    }
}