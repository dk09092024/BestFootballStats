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
            .MustAsync(async (id, token) => await teamRepository.ExistsAsync(id))
            .WithMessage("Team not found.");
    }
}