using Domain.Repositories;
using FluentValidation;

namespace Domain.Features.Teams.Get;

public class GetTeamValidator : AbstractValidator<GetTeamQuery>
{
    public GetTeamValidator(ITeamRepository teamRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id is required.")
            .MustAsync(async (id, cancellationToken) => await teamRepository.ExistsAsync(id, cancellationToken))
            .WithMessage("Team not found.");
    }
}